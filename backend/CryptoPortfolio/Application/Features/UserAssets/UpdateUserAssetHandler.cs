using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.UserAssets;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.UserAssets
{
    internal sealed class UpdateUserAssetHandler(
        IUserAssetRepository userAssetRepository,
        ICurrencyRepository currencyRepository,
        ITransactionTypeRepository transactionTypeRepository,
        IUserAssetTransactionRepository userAssetTransactionRepository,
        IDbTransactionService dbTransactionService,
        IValidator<UpdateUserAsset> validator,
        ILogger<UpdateUserAssetHandler> logger)
        : AbstractHandler<UpdateUserAsset, UserAsset>(validator, logger)
    {
        protected override async Task<HandlerResponse<UserAsset>> HandleRequest(
            UpdateUserAsset request,
            CancellationToken cancellationToken)
        {
            var existing = await userAssetRepository.GetByUserAndAssetAsync(
                request.UserId,
                request.AssetId,
                request.CurrencyId,
                cancellationToken);

            if (existing is null)
            {
                return HandlerResponse<UserAsset>.NotFound("User asset not found.");
            }

            var newQuantity = request.Quantity;
            if (newQuantity < 0)
            {
                return HandlerResponse<UserAsset>.BadRequest("Quantity cannot be negative.");
            }

            var delta = newQuantity - existing.Quantity;
            if (delta == 0)
            {
                return HandlerResponse<UserAsset>.Ok(existing);
            }

            var currency = await currencyRepository.GetByIdAsync(request.CurrencyId, cancellationToken);
            if (currency is null)
            {
                return HandlerResponse<UserAsset>.NotFound("Currency not found.");
            }

            await dbTransactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                var updated = await userAssetRepository.Update(existing with
                {
                    Quantity = newQuantity,
                }, cancellationToken);

                if (updated is null)
                {
                    await dbTransactionService.RollbackAsync(cancellationToken);
                    return HandlerResponse<UserAsset>.NotFound("User asset not found.");
                }

                var transactionTypeCode = delta > 0
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
                    AssetSymbol = updated.AssetSymbol,
                    CurrencyId = request.CurrencyId,
                    CurrencySymbol = currency.Symbol,
                    TransactionTypeId = transactionType.Id,
                    TransactionTypeCode = transactionType.Code,
                    Quantity = Math.Abs(delta),
                    Amount = Math.Abs(delta) * request.Price,
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
                logger.LogError("Failed to update user asset for UserId {UserId} AssetId {AssetId}", request.UserId, request.AssetId);
                return HandlerResponse<UserAsset>.Fail("Failed to update user asset.");
            }
        }
    }
}
