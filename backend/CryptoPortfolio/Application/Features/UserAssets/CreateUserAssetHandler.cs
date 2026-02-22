using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Mapping;
using CryptoPorfolio.Application.Requests.UserAssets;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.UserAssets
{
    internal sealed class CreateUserAssetHandler(
        IUserAssetRepository userAssetRepository,
        IAssetRepository assetRepository,
        ICurrencyRepository currencyRepository,
        ITransactionTypeRepository transactionTypeRepository,
        IUserAssetTransactionRepository userAssetTransactionRepository,
        IDbTransactionService dbTransactionService,
        IValidator<AddUserAsset> validator,
        ILogger<CreateUserAssetHandler> logger)
        : AbstractHandler<AddUserAsset, UserAsset>(validator, logger)
    {
        protected override async Task<HandlerResponse<UserAsset>> HandleRequest(
            AddUserAsset request,
            CancellationToken cancellationToken)
        {
            var asset = await assetRepository.GetByIdAsync(request.AssetId, cancellationToken);
            if (asset is null)
            {
                return HandlerResponse<UserAsset>.NotFound("Asset not found.");
            }

            var currency = await currencyRepository.GetByIdAsync(request.CurrencyId, cancellationToken);
            if (currency is null)
            {
                return HandlerResponse<UserAsset>.NotFound("Currency not found.");
            }

            var existing = await userAssetRepository.GetByUserAndAssetAsync(
                request.UserId,
                request.AssetId,
                request.CurrencyId,
                cancellationToken);

            if (existing is not null)
            {
                return new HandlerResponse<UserAsset>
                {
                    Success = false,
                    Error = "User asset already exists.",
                    StatusCode = 409,
                };
            }

            await dbTransactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                var added = await userAssetRepository.Create(
                    request.ToModel(asset) with { CurrencySymbol = currency.Symbol },
                    cancellationToken);

                if (added is null)
                {
                    await dbTransactionService.RollbackAsync(cancellationToken);
                    return HandlerResponse<UserAsset>.Fail("Failed to create user asset.");
                }

                var transactionType = await transactionTypeRepository.GetByCodeAsync(
                    Application.Models.TransactionTypes.Buy,
                    cancellationToken);
                if (transactionType is null)
                {
                    await dbTransactionService.RollbackAsync(cancellationToken);
                    return HandlerResponse<UserAsset>.Fail("Transaction type not found.");
                }

                var transaction = new UserAssetTransaction
                {
                    UserId = request.UserId,
                    UserAssetId = added.Id,
                    AssetId = request.AssetId,
                    AssetSymbol = asset.Symbol,
                    CurrencyId = request.CurrencyId,
                    CurrencySymbol = currency.Symbol,
                    TransactionTypeId = transactionType.Id,
                    TransactionTypeCode = transactionType.Code,
                    Quantity = request.Quantity,
                    Amount = request.Quantity * request.Price,
                    Price = request.Price,
                    ExecutedAt = request.ExecutedAt ?? DateTime.UtcNow,
                };

                await userAssetTransactionRepository.CreateAsync(transaction, cancellationToken);

                await dbTransactionService.CommitAsync(cancellationToken);

                return HandlerResponse<UserAsset>.Ok(added);
            }
            catch (Exception)
            {
                await dbTransactionService.RollbackAsync(cancellationToken);
                logger.LogError("Failed to create user asset for UserId {UserId} AssetId {AssetId}", request.UserId, request.AssetId);
                return HandlerResponse<UserAsset>.Fail("Failed to create user asset.");
            }
        }
    }
}
