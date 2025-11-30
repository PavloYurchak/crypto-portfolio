// <copyright file="UserAssetMappingProfile.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Infrastructure.Mapping
{
    internal static class UserAssetMappingProfile
    {
        public static UserAsset ToModel(this Entities.UserAsset entity)
        {
            return new UserAsset
            {
                Id = entity.Id,
                UserId = entity.UserId,
                AssetId = entity.AssetId,
                AssetSymbol = entity.Asset?.Symbol ?? string.Empty,
                AssetName = entity.Asset?.Name ?? string.Empty,
                Quantity = entity.Quantity,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }

        public static Entities.UserAsset ToEntity(this UserAsset model)
        {
            return new Entities.UserAsset
            {
                UserId = model.UserId,
                AssetId = model.AssetId,
                Quantity = model.Quantity,
            };
        }
    }
}
