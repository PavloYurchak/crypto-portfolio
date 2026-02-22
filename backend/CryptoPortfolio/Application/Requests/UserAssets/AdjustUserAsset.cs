using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Requests.UserAssets
{
    public sealed record AdjustUserAsset : IHandlerRequest<UserAsset>
    {
        public int UserId { get; init; }

        public int AssetId { get; init; }

        public int CurrencyId { get; init; }

        public decimal DeltaQuantity { get; init; }

        public decimal Price { get; init; }

        public DateTime? ExecutedAt { get; init; }
    }
}
