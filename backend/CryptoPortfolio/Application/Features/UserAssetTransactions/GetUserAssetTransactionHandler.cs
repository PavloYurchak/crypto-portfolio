using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.UserAssetTransactions;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.UserAssetTransactions
{
    internal sealed class GetUserAssetTransactionHandler(
        IUserAssetTransactionRepository userAssetTransactionRepository,
        IValidator<GetUserAssetTransaction> validator,
        ILogger<GetUserAssetTransactionHandler> logger)
        : AbstractHandler<GetUserAssetTransaction, UserAssetTransaction>(validator, logger)
    {
        protected override async Task<HandlerResponse<UserAssetTransaction>> HandleRequest(
            GetUserAssetTransaction request,
            CancellationToken cancellationToken)
        {
            var transaction = await userAssetTransactionRepository.GetByIdAsync(
                request.TransactionId,
                cancellationToken);

            if (transaction is null || transaction.UserId != request.UserId)
            {
                return HandlerResponse<UserAssetTransaction>.NotFound("Transaction not found.");
            }

            return HandlerResponse<UserAssetTransaction>.Ok(transaction);
        }
    }
}
