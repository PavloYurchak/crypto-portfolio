
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Infrastructure.Context;
using CryptoPorfolio.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CryptoPorfolio.Infrastructure.Repositories
{
    internal sealed class CurrencyRepository(CryptoPorfolioContext context) : ICurrencyRepository
    {
        public async Task<Currency> CreateAsync(Currency currency, CancellationToken cancellationToken = default)
        {
            var entity = currency.ToEntity();

            entity.CreatedAt = DateTime.UtcNow;
            context.Currencies.Add(entity);

            await context.SaveChangesAsync(cancellationToken);
            return entity.ToModel();
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await context.Currencies
                .SingleOrDefaultAsync(e => e.Id == id && e.DeletedAt == null, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            entity.DeletedAt = DateTime.UtcNow;
            context.Currencies.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IReadOnlyCollection<Currency>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Currencies
                .AsNoTracking()
                .Where(e => e.DeletedAt == null && e.IsActive)
                .OrderBy(e => e.Symbol)
                .Select(e => e.ToModel())
                .ToListAsync(cancellationToken);
        }

        public async Task<Currency?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await context.Currencies
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id && e.DeletedAt == null, cancellationToken);

            return entity?.ToModel();
        }

        public async Task<Currency?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken = default)
        {
            var entity = await context.Currencies
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Symbol == symbol && e.DeletedAt == null, cancellationToken);

            return entity?.ToModel();
        }

        public async Task<Currency?> UpdateAsync(Currency currency, CancellationToken cancellationToken = default)
        {
            var entity = await context.Currencies
                .SingleOrDefaultAsync(e => e.Id == currency.Id && e.DeletedAt == null, cancellationToken);

            if (entity != null)
            {
                entity.Name = currency.Name;
                entity.Symbol = currency.Symbol;
                entity.IsActive = currency.IsActive;
                entity.UpdatedAt = DateTime.UtcNow;

                context.Currencies.Update(entity);
                await context.SaveChangesAsync(cancellationToken);
            }

            return entity?.ToModel();
        }
    }
}
