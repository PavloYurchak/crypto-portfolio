using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.UserAssets;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.UserAssets
{
    internal sealed class AdjustUserAssetHandler(
        IUserAssetRepository userAssetRepository,
        IAssetRepository assetRepository,
        ICurrencyRepository currencyRepository,
        ITransactionTypeRepository transactionTypeRepository,
        IUserAssetTransactionRepository userAssetTransactionRepository,
        IDbTransactionService dbTransactionService,
        IValidator<AdjustUserAsset> validator,
        ILogger<AdjustUserAssetHandler> logger)
        : AbstractHandler<AdjustUserAsset, UserAsset>(validator, logger)
    {
        protected override async Task<HandlerResponse<UserAsset>> HandleRequest(
            AdjustUserAsset request,
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

            if (existing is null && request.DeltaQuantity < 0)
            {
                return HandlerResponse<UserAsset>.NotFound("User asset not found.");
            }

            var newQuantity = (existing?.Quantity ?? 0) + request.DeltaQuantity;
            if (newQuantity < 0)
            {
                return HandlerResponse<UserAsset>.BadRequest("Quantity cannot be negative.");
            }

            await dbTransactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                UserAsset updated;
                if (existing is null)
                {
                    var created = await userAssetRepository.Create(
                        new UserAsset
                        {
                            UserId = request.UserId,
                            AssetId = request.AssetId,
                            CurrencyId = request.CurrencyId,
                            AssetSymbol = asset.Symbol,
                            AssetName = asset.Name,
                            CurrencySymbol = currency.Symbol,
                            Quantity = newQuantity,
                        },
                        cancellationToken);

                    if (created is null)
                    {
                        await dbTransactionService.RollbackAsync(cancellationToken);
                        return HandlerResponse<UserAsset>.Fail("Failed to create user asset.");
                    }

                    updated = created;
                }
                else
                {
                    var updateModel = existing with
                    {
                        Quantity = newQuantity,
                    };

                    var updatedResult = await userAssetRepository.Update(updateModel, cancellationToken);
                    if (updatedResult is null)
                    {
                        await dbTransactionService.RollbackAsync(cancellationToken);
                        return HandlerResponse<UserAsset>.NotFound("User asset not found.");
                    }

                    updated = updatedResult;
                }

                var transactionTypeCode = request.DeltaQuantity > 0
                    ? Application.Models.TransactionTypes.Buy
                    : Application.Models.TransactionTypes.Sell;

                var transactionType = await transactionTypeRepository.GetByCodeAsync(
                    transactionTypeCode,
                    cancellationToken);

                if (transactionType is null)
                {
                    await dbTransactionService.RollbackAsync(cancellationToken);
                    return HandlerResponse<UserAsset>.Fail("Transaction type not found.");
                }

                var transaction = new UserAssetTransaction
                {
                    UserId = request.UserId,
                    UserAssetId = updated.Id,
                    AssetId = request.AssetId,
                    AssetSymbol = asset.Symbol,
                    CurrencyId = request.CurrencyId,
                    CurrencySymbol = currency.Symbol,
                    TransactionTypeId = transactionType.Id,
                    TransactionTypeCode = transactionType.Code,
                    Quantity = Math.Abs(request.DeltaQuantity),
                    Amount = Math.Abs(request.DeltaQuantity) * request.Price,
                    Price = request.Price,
                    ExecutedAt = request.ExecutedAt ?? DateTime.UtcNow,
                };

                await userAssetTransactionRepository.CreateAsync(transaction, cancellationToken);
                await dbTransactionService.CommitAsync(cancellationToken);

                return HandlerResponse<UserAsset>.Ok(updated);
            }
            catch (Exception)
            {
                await dbTransactionService.RollbackAsync(cancellationToken);
                logger.LogError(
                    "Failed to adjust user asset for UserId {UserId} AssetId {AssetId}",
                    request.UserId,
                    request.AssetId);
                return HandlerResponse<UserAsset>.Fail("Failed to adjust user asset.");
            }
        }
    }
}
