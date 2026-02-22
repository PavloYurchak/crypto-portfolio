using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Requests.UserAssetTransactions
{
    public sealed record GetUserAssetTransaction(long TransactionId, int UserId)
        : IHandlerRequest<UserAssetTransaction>;
}
