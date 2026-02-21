
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Infrastructure.Context;
using CryptoPorfolio.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CryptoPorfolio.Infrastructure.Repositories
{
    internal sealed class AssetRepository(CryptoPorfolioContext context) : IAssetRepository
    {
        public async Task<Asset?> CreateAssetAsync(Asset model, CancellationToken cancellationToken = default)
        {
            var entity = model.ToEntity();

            entity.CreatedAt = DateTime.UtcNow;

            await context.Assets.AddAsync(entity);

            await context.SaveChangesAsync(cancellationToken);
            return entity.ToModel();
        }

        public async Task<bool> DeleteAssetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Assets
                .Where(e => e.Id == id && e.DeletedAt == null)
                .ExecuteUpdateAsync(e => e.SetProperty(p => p.DeletedAt, p => DateTime.UtcNow), cancellationToken)
                .ContinueWith(t => t.Result > 0, cancellationToken);
        }

        public async Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken = default)
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

        public async Task<Asset?> UpdateAssetAsync(Asset model, CancellationToken cancellationToken = default)
        {
            var entity = await context.Assets
                .SingleOrDefaultAsync(
                    e =>
                    e.Id == model.Id &&
                    !e.DeletedAt.HasValue,
                    cancellationToken);

            if (entity is not null)
            {
                entity.Symbol = model.Symbol;
                entity.Name = model.Name;
                entity.IsActive = model.IsActive;
                entity.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync(cancellationToken);
            }

            return entity?.ToModel();
        }
    }
}
