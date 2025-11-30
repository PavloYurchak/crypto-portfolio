// <copyright file="UserAssetTransactionMappingProfile.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Infrastructure.Mapping
{
    internal static class UserAssetTransactionMappingProfile
    {
        public static UserAssetTransaction ToModel(this Entities.UserAssetTransaction entity)
        {
            return new UserAssetTransaction
            {
                Id = entity.Id,
                UserId = entity.UserId,
                UserAssetId = entity.UserAssetId,
                AssetId = entity.AssetId,
                AssetSymbol = entity.Asset?.Symbol ?? string.Empty,
                CurrencyId = entity.CurrencyId,
                CurrencySymbol = entity.Currency?.Symbol ?? string.Empty,
                TransactionTypeCode = entity.Type?.Code ?? string.Empty,
                TransactionTypeId = entity.TypeId,
                Quantity = entity.Quantity,
                Amount = entity.Amount,
                Price = entity.Price,
                ExecutedAt = entity.ExecutedAt,
                CreatedAt = entity.CreatedAt,
            };
        }

        public static Entities.UserAssetTransaction ToEntity(this UserAssetTransaction model)
        {
            return new Entities.UserAssetTransaction
            {
                UserId = model.UserId,
                UserAssetId = model.UserAssetId,
                AssetId = model.AssetId,
                CurrencyId = model.CurrencyId,
                TypeId = model.TransactionTypeId,
                Quantity = model.Quantity,
                Amount = model.Amount,
                Price = model.Price,
                ExecutedAt = model.ExecutedAt,
            };
        }
    }
}
