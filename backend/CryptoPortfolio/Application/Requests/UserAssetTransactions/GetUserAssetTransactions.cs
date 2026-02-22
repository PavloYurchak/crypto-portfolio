using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Requests.UserAssetTransactions
{
    public sealed record GetUserAssetTransactions(int UserId, int AssetId, int CurrencyId)
        : IHandlerRequest<IEnumerable<UserAssetTransaction>>;
}
