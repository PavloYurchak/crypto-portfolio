
namespace CryptoPorfolio.Domain.Models
{
    public sealed record UserAssetTransaction : AbstractModel
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public long UserAssetId { get; set; }

        public int AssetId { get; set; }

        public required string AssetSymbol { get; set; }

        public int CurrencyId { get; set; }

        public required string CurrencySymbol { get; set; }

        public string PairSymbol => $"{AssetSymbol}/{CurrencySymbol}";

        public int TransactionTypeId { get; set; }

        public required string TransactionTypeCode { get; set; }

        public decimal Quantity { get; set; }

        public decimal Amount { get; set; }

        public decimal Price { get; set; }

        public DateTime ExecutedAt { get; set; }
    }
}
