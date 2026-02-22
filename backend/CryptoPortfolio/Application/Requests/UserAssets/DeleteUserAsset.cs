using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.UserAssets
{
    public sealed record DeleteUserAsset(int UserId, int AssetId) : IHandlerRequest<bool>;
}
