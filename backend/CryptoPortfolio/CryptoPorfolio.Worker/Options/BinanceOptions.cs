namespace CryptoPorfolio.Worker.Options
{
    public sealed class BinanceOptions
    {
        public const string HttpClientName = "Binance";

        public string BaseUrl { get; init; } = "https://api.binance.com";

        public string ExchangeInfoPath { get; init; } = "/api/v3/exchangeInfo";
    }
}
