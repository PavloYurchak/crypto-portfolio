using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Abstractions.Services;
using CryptoPorfolio.Application.Mapping;
using CryptoPorfolio.Application.Requests.UserAssetTransactions;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Domain.Services;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Services
{
    internal sealed class UserAssetTransactionService(
        IUserAssetRepository userAssetRepository,
        IUserAssetTransactionRepository userAssetTransactionRepository,
        ICurrencyRepository currencyRepository,
        ITransactionTypeRepository transactionTypeRepository,
        IDbTransactionService dbTransactionService,
        ILogger<UserAssetTransactionService> logger)
        : IUserAssetTransactionService
    {
        public async Task<HandlerResponse<UserAssetTransaction>> CreateAsync(
            AddUserAssetTransaction request,
            CancellationToken cancellationToken = default)
        {
            var userAsset = await userAssetRepository.GetByUserAndAssetAsync(
                request.UserId,
                request.AssetId,
                cancellationToken);

            if (userAsset is null)
            {
                return HandlerResponse<UserAssetTransaction>.NotFound("User asset not found.");
            }

            var currency = await currencyRepository.GetByIdAsync(request.CurrencyId, cancellationToken);
            if (currency is null)
            {
                return HandlerResponse<UserAssetTransaction>.NotFound("Currency not found.");
            }

            var transactionType = await transactionTypeRepository.GetByCodeAsync(
                request.TransactionTypeCode,
                cancellationToken);
            if (transactionType is null)
            {
                return HandlerResponse<UserAssetTransaction>.NotFound("Transaction type not found.");
            }

            await dbTransactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                var model = request.ToModel(userAsset, currency, transactionType);
                var created = await userAssetTransactionRepository.CreateAsync(model, cancellationToken);

                await dbTransactionService.CommitAsync(cancellationToken);
                return HandlerResponse<UserAssetTransaction>.Ok(created);
            }
            catch (Exception)
            {
                await dbTransactionService.RollbackAsync(cancellationToken);
                logger.LogError(
                    "Failed to create user asset transaction for UserId {UserId} AssetId {AssetId}",
                    request.UserId,
                    request.AssetId);
                return HandlerResponse<UserAssetTransaction>.Fail("Failed to create user asset transaction.");
            }
        }
    }
}
