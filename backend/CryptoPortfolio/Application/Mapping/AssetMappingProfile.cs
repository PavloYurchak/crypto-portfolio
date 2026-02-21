
using CryptoPorfolio.Application.Requests.Assets;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Mapping
{
    internal static class AssetMappingProfile
    {
        public static Asset ToModel(this AddAsset asset)
        {
            return new Asset
            {
                Symbol = asset.Symbol,
                Name = asset.Name,
            };
        }

        public static Asset ToModel(this UpdateAsset asset)
        {
            return new Asset
            {
                Id = asset.Id,
                Symbol = asset.Symbol,
                Name = asset.Name,
            };
        }
    }
}
