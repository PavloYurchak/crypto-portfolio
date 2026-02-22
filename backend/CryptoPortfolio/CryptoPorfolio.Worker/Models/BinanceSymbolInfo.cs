using System.Text.Json.Serialization;

namespace CryptoPorfolio.Worker.Models
{
    public sealed class BinanceSymbolInfo
    {
        [JsonPropertyName("baseAsset")]
        public string BaseAsset { get; init; } = string.Empty;
    }
}
