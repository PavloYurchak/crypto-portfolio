// <copyright file="UserAssetRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Infrastructure.Context;
using CryptoPorfolio.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CryptoPorfolio.Infrastructure.Repositories
{
    internal sealed class UserAssetRepository(CryptoPorfolioContext context)
    : IUserAssetRepository
    {
        public async Task<IReadOnlyCollection<UserAsset>> GetByUserAsync(
            int userId,
            CancellationToken cancellationToken = default)
        {
            return await context.UserAssets
                .AsNoTracking()
                .Include(e => e.Asset)
                .Where(e => e.UserId == userId && e.DeletedAt == null)
                .OrderBy(e => e.Asset.Symbol)
                .Select(e => e.ToModel())
                .ToListAsync(cancellationToken);
        }

        public async Task<UserAsset?> GetByUserAndAssetAsync(
            int userId,
            int assetId,
            CancellationToken cancellationToken = default)
        {
            var entity = await context.UserAssets
                .AsNoTracking()
                .Include(e => e.Asset)
                .SingleOrDefaultAsync(
                    e =>
                    e.UserId == userId &&
                    e.AssetId == assetId &&
                    e.DeletedAt == null,
                    cancellationToken);

            return entity?.ToModel();
        }

        public async Task<UserAsset> Create(UserAsset model, CancellationToken cancellationToken = default)
        {
            var entity = model.ToEntity();
            entity.CreatedAt = DateTime.UtcNow;

            await context.UserAssets.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            entity = await context.UserAssets
                .Include(e => e.Asset)
                .SingleAsync(e => e.Id == entity.Id, cancellationToken);

            return entity.ToModel();
        }

        public async Task<UserAsset> Update(UserAsset model, CancellationToken cancellationToken = default)
        {
            var entity = await context.UserAssets
                .SingleOrDefaultAsync(
                    e =>
                    e.Id == model.Id &&
                    !e.DeletedAt.HasValue,
                    cancellationToken);

            if (entity is not null)
            {
                entity.Quantity = model.Quantity;
                entity.AssetId = model.AssetId;
                entity.UserId = model.UserId;
                entity.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync(cancellationToken);
            }

            entity = await context.UserAssets
                .Include(e => e.Asset)
                .SingleAsync(e => e.Id == entity.Id, cancellationToken);

            return entity.ToModel();
        }
    }
}
