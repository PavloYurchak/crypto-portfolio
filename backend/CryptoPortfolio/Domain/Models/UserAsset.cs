
namespace CryptoPorfolio.Domain.Models
{
    public sealed record UserAsset : AbstractModel
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public int AssetId { get; set; }

        public int CurrencyId { get; set; }

        public required string AssetSymbol { get; set; }

        public required string AssetName { get; set; }

        public required string CurrencySymbol { get; set; }

        public decimal Quantity { get; set; }
    }
}
