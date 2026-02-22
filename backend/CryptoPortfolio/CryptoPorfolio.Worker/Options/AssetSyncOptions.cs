namespace CryptoPorfolio.Worker.Options
{
    public sealed class AssetSyncOptions
    {
        public int IntervalHours { get; init; } = 24;

        public int FreezeMinutes { get; init; } = 60;
    }
}
