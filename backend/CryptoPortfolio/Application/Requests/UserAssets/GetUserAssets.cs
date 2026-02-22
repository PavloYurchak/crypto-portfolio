using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Requests.UserAssets
{
    public sealed record GetUserAssets(int UserId) : IHandlerRequest<IEnumerable<UserAsset>>;
}
