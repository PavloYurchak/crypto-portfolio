
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Infrastructure.Context;
using CryptoPorfolio.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CryptoPorfolio.Infrastructure.Repositories
{
    internal sealed class UserAssetTransactionRepository(
    CryptoPorfolioContext context)
    : IUserAssetTransactionRepository
    {
        public async Task<IEnumerable<UserAssetTransaction>> GetUserTransactionsAsync(
            int userAssetId,
            CancellationToken cancellationToken = default)
        {
            var result = await context.UserAssetTransactions
                .AsNoTracking()
                .Include(e => e.Asset)
                .Include(e => e.Currency)
                .Include(e => e.Type)
                .Where(e => e.UserAssetId == userAssetId)
                .ToListAsync();

            return result.Select(e => e.ToModel());
        }

        public async Task<UserAssetTransaction?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = await context.UserAssetTransactions
                .AsNoTracking()
                .Include(e => e.Asset)
                .Include(e => e.Currency)
                .Include(e => e.Type)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

            return entity?.ToModel();
        }

        public async Task<UserAssetTransaction> CreateAsync(
            UserAssetTransaction model,
            CancellationToken cancellationToken = default)
        {
            var entity = model.ToEntity();
            entity.CreatedAt = DateTime.UtcNow;

            await context.UserAssetTransactions.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            entity = await context.UserAssetTransactions
                .Include(e => e.Asset)
                .Include(e => e.Currency)
                .Include(e => e.Type)
                .SingleAsync(e => e.Id == entity.Id, cancellationToken);

            return entity.ToModel();
        }
    }
}
