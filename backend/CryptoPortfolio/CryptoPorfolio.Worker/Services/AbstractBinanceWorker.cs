using System.Net;
using System.Net.Http.Json;
using CryptoPorfolio.Worker.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CryptoPorfolio.Worker.Services
{
    public abstract class AbstractBinanceWorker : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        private readonly BinanceOptions _binanceOptions;
        private readonly AssetSyncOptions _syncOptions;
        private DateTimeOffset? _freezeUntilUtc;

        protected AbstractBinanceWorker(
            IHttpClientFactory httpClientFactory,
            IOptions<BinanceOptions> binanceOptions,
            IOptions<AssetSyncOptions> syncOptions,
            ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _binanceOptions = binanceOptions.Value;
            _syncOptions = syncOptions.Value;
        }

        protected BinanceOptions BinanceConfig => _binanceOptions;

        protected abstract Task ExecuteOnceAsync(CancellationToken stoppingToken);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await RunOnceWithRecovery(stoppingToken);

            var interval = TimeSpan.FromHours(Math.Max(1, _syncOptions.IntervalHours));
            using var timer = new PeriodicTimer(interval);
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await RunOnceWithRecovery(stoppingToken);
            }
        }

        protected async Task<T?> GetJsonAsync<T>(string path, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient(BinanceOptions.HttpClientName);
            var normalizedPath = NormalizePath(path);

            using var response = await client.GetAsync(normalizedPath, cancellationToken);
            if (!await HandleHttpResponseAsync(response, cancellationToken))
            {
                return default;
            }

            return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
        }

        protected async Task<bool> HandleHttpResponseAsync(
            HttpResponseMessage response,
            CancellationToken cancellationToken)
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var status = response.StatusCode;
            var body = await SafeReadBodyAsync(response, cancellationToken);

            if (status == (HttpStatusCode)423)
            {
                FreezeServices();
                _logger.LogWarning("Binance returned 423. Services frozen until {FreezeUntilUtc}. Body: {Body}", _freezeUntilUtc, body);
                return false;
            }

            if ((int)status >= 500)
            {
                _logger.LogWarning(
                    "Binance returned {StatusCode}. Server error; execution status unknown. Body: {Body}",
                    (int)status,
                    body);
                return false;
            }

            switch (status)
            {
                case HttpStatusCode.Forbidden:
                    _logger.LogWarning("Binance returned 403. Possible WAF/rate limit/security block. Body: {Body}", body);
                    break;
                case HttpStatusCode.Conflict:
                    _logger.LogWarning("Binance returned 409. Partial success possible. Body: {Body}", body);
                    break;
                case (HttpStatusCode)429:
                    _logger.LogWarning("Binance returned 429. Rate limit exceeded. Body: {Body}", body);
                    break;
                case (HttpStatusCode)418:
                    _logger.LogWarning("Binance returned 418. IP banned for rate limit violations. Body: {Body}", body);
                    break;
                default:
                    if ((int)status >= 400)
                    {
                        _logger.LogWarning("Binance returned {StatusCode}. Client error. Body: {Body}", (int)status, body);
                    }
                    else
                    {
                        _logger.LogWarning("Binance returned {StatusCode}. Body: {Body}", (int)status, body);
                    }
                    break;
            }

            return false;
        }

        private async Task RunOnceWithRecovery(CancellationToken stoppingToken)
        {
            if (IsFrozen())
            {
                _logger.LogWarning("Binance worker is frozen until {FreezeUntilUtc}. Skipping this run.", _freezeUntilUtc);
                return;
            }

            try
            {
                await ExecuteOnceAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Binance worker canceled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Binance worker run failed.");
            }
        }

        private bool IsFrozen()
        {
            if (_freezeUntilUtc is null)
            {
                return false;
            }

            if (DateTimeOffset.UtcNow >= _freezeUntilUtc)
            {
                _freezeUntilUtc = null;
                return false;
            }

            return true;
        }

        private void FreezeServices()
        {
            var freezeMinutes = Math.Max(1, _syncOptions.FreezeMinutes);
            _freezeUntilUtc = DateTimeOffset.UtcNow.AddMinutes(freezeMinutes);
        }

        private static string NormalizePath(string path)
            => string.IsNullOrWhiteSpace(path) ? "/api/v3/exchangeInfo" : (path.StartsWith('/') ? path : "/" + path);

        private static async Task<string?> SafeReadBodyAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            try
            {
                return await response.Content.ReadAsStringAsync(cancellationToken);
            }
            catch
            {
                return null;
            }
        }
    }
}
