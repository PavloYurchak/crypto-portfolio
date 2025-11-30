// <copyright file="AssetRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Infrastructure.Context;
using CryptoPorfolio.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CryptoPorfolio.Infrastructure.Repositories
{
    internal sealed class AssetRepository(CryptoPorfolioContext context) : IAssetRepository
    {
        public async Task<IReadOnlyCollection<Asset>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Assets
                .AsNoTracking()
                .Where(e => e.DeletedAt == null && e.IsActive)
                .OrderBy(e => e.Symbol)
                .Select(e => e.ToModel())
                .ToListAsync(cancellationToken);
        }

        public async Task<Asset?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await context.Assets
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id && e.DeletedAt == null, cancellationToken);

            return entity?.ToModel();
        }

        public async Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken = default)
        {
            var entity = await context.Assets
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Symbol == symbol && e.DeletedAt == null, cancellationToken);

            return entity?.ToModel();
        }
    }
}
