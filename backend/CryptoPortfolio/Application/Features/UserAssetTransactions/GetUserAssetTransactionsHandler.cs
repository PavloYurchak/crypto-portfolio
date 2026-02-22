using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.UserAssetTransactions;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.UserAssetTransactions
{
    internal sealed class GetUserAssetTransactionsHandler(
        IUserAssetRepository userAssetRepository,
        IUserAssetTransactionRepository userAssetTransactionRepository,
        IValidator<GetUserAssetTransactions> validator,
        ILogger<GetUserAssetTransactionsHandler> logger)
        : AbstractHandler<GetUserAssetTransactions, IEnumerable<UserAssetTransaction>>(validator, logger)
    {
        protected override async Task<HandlerResponse<IEnumerable<UserAssetTransaction>>> HandleRequest(
            GetUserAssetTransactions request,
            CancellationToken cancellationToken)
        {
            var userAsset = await userAssetRepository.GetByUserAndAssetAsync(
                request.UserId,
                request.AssetId,
                cancellationToken);

            if (userAsset is null)
            {
                return HandlerResponse<IEnumerable<UserAssetTransaction>>.NotFound("User asset not found.");
            }

            var transactions = await userAssetTransactionRepository.GetUserTransactionsAsync(
                userAsset.Id,
                cancellationToken);

            return HandlerResponse<IEnumerable<UserAssetTransaction>>.Ok(transactions);
        }
    }
}
