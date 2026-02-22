using System.Text.Json.Serialization;

namespace CryptoPorfolio.Worker.Models
{
    public sealed class BinanceExchangeInfoResponse
    {
        [JsonPropertyName("symbols")]
        public List<BinanceSymbolInfo> Symbols { get; init; } = new();
    }
}
