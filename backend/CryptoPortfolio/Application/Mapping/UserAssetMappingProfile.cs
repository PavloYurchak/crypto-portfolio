using CryptoPorfolio.Application.Requests.UserAssets;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Mapping
{
    internal static class UserAssetMappingProfile
    {
        public static UserAsset ToModel(this AddUserAsset request, Asset asset)
        {
            return new UserAsset
            {
                UserId = request.UserId,
                AssetId = request.AssetId,
                CurrencyId = request.CurrencyId,
                AssetSymbol = asset.Symbol,
                AssetName = asset.Name,
                CurrencySymbol = string.Empty,
                Quantity = request.Quantity,
            };
        }

        public static UserAsset ToModel(this UpdateUserAsset request, UserAsset existing)
        {
            return new UserAsset
            {
                Id = existing.Id,
                UserId = request.UserId,
                AssetId = existing.AssetId,
                CurrencyId = existing.CurrencyId,
                AssetSymbol = existing.AssetSymbol,
                AssetName = existing.AssetName,
                CurrencySymbol = existing.CurrencySymbol,
                Quantity = request.Quantity,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt,
            };
        }
    }
}
