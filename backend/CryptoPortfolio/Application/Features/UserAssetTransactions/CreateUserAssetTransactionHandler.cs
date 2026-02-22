using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Abstractions.Services;
using CryptoPorfolio.Application.Requests.UserAssetTransactions;
using CryptoPorfolio.Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.UserAssetTransactions
{
    internal sealed class CreateUserAssetTransactionHandler(
        IUserAssetTransactionService userAssetTransactionService,
        IValidator<AddUserAssetTransaction> validator,
        ILogger<CreateUserAssetTransactionHandler> logger)
        : AbstractHandler<AddUserAssetTransaction, UserAssetTransaction>(validator, logger)
    {
        protected override Task<HandlerResponse<UserAssetTransaction>> HandleRequest(
            AddUserAssetTransaction request,
            CancellationToken cancellationToken)
        {
            return userAssetTransactionService.CreateAsync(request, cancellationToken);
        }
    }
}
