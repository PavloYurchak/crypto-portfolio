using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Worker.Models;
using CryptoPorfolio.Worker.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CryptoPorfolio.Worker.Services
{
    public sealed class BinanceAssetSyncWorker : AbstractBinanceWorker
    {
        private readonly IAssetRepository _assetRepository;
        private readonly ILogger<BinanceAssetSyncWorker> _logger;

        public BinanceAssetSyncWorker(
            IHttpClientFactory httpClientFactory,
            IAssetRepository assetRepository,
            IOptions<BinanceOptions> binanceOptions,
            IOptions<AssetSyncOptions> syncOptions,
            ILogger<BinanceAssetSyncWorker> logger)
            : base(httpClientFactory, binanceOptions, syncOptions, logger)
        {
            _assetRepository = assetRepository;
            _logger = logger;
        }

        protected override async Task ExecuteOnceAsync(CancellationToken stoppingToken)
        {
            await SyncAssets(stoppingToken);
        }

        private async Task SyncAssets(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching Binance exchange info.");

            var response = await GetJsonAsync<BinanceExchangeInfoResponse>(
                BinanceConfig.ExchangeInfoPath,
                cancellationToken);

            if (response is null || response.Symbols.Count == 0)
            {
                _logger.LogWarning("Binance exchange info response is empty.");
                return;
            }

            var symbols = response.Symbols
                .Select(s => s.BaseAsset)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var created = 0;
            var updated = 0;

            foreach (var symbol in symbols)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existing = await _assetRepository.GetBySymbolAsync(symbol, cancellationToken);
                if (existing is null)
                {
                    var model = new Asset
                    {
                        Symbol = symbol,
                        Name = symbol,
                        IsActive = true,
                    };

                    await _assetRepository.CreateAssetAsync(model, cancellationToken);
                    created++;
                    continue;
                }

                if (!existing.IsActive || !string.Equals(existing.Name, symbol, StringComparison.Ordinal))
                {
                    var updateModel = new Asset
                    {
                        Id = existing.Id,
                        Symbol = symbol,
                        Name = symbol,
                        IsActive = true,
                    };

                    await _assetRepository.UpdateAssetAsync(updateModel, cancellationToken);
                    updated++;
                }
            }

            _logger.LogInformation(
                "Binance asset sync completed. Total: {Total}, Created: {Created}, Updated: {Updated}",
                symbols.Count,
                created,
                updated);
        }
    }
}
