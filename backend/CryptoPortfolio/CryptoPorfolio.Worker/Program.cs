
using CryptoPorfolio.Infrastructure;
using CryptoPorfolio.Worker.Options;
using CryptoPorfolio.Worker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<BinanceOptions>(builder.Configuration.GetSection("Binance"));
builder.Services.Configure<AssetSyncOptions>(builder.Configuration.GetSection("AssetSync"));

builder.Services.AddHttpClient(BinanceOptions.HttpClientName, (sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<BinanceOptions>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl);
});

builder.Services.AddHostedService<BinanceAssetSyncWorker>();

var host = builder.Build();
await host.RunAsync();
