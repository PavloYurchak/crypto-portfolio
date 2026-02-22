using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Requests.UserAssets
{
    public sealed record UpdateUserAsset : IHandlerRequest<UserAsset>
    {
        public int UserId { get; init; }

        public int AssetId { get; init; }

        public decimal Quantity { get; init; }
    }
}
