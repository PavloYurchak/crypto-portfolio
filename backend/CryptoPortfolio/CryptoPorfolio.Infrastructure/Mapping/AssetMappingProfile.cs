// <copyright file="AssetMappingProfile.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Infrastructure.Mapping
{
    internal static class AssetMappingProfile
    {
        public static Asset ToModel(this Entities.Asset entity)
        {
            return new Asset
            {
                Id = entity.Id,
                Symbol = entity.Symbol,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
            };
        }

        public static Entities.Asset ToEntity(this Asset model)
        {
            return new Entities.Asset
            {
                Symbol = model.Symbol,
                Name = model.Name,
                IsActive = model.IsActive,
            };
        }
    }
}
