
using CryptoPorfolio.Domain.Services;
using CryptoPorfolio.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace CryptoPorfolio.Infrastructure.Services
{
    internal class DbTransactionService(CryptoPorfolioContext context) : IDbTransactionService
    {
        private IDbContextTransaction? dbTransactionService;

        public async Task BeginTransactionAsync(CancellationToken ct)
        {
            dbTransactionService = await context.Database.BeginTransactionAsync(ct);
        }

        public async Task CommitAsync(CancellationToken ct)
        {
            if (dbTransactionService != null)
            {
                await dbTransactionService.CommitAsync(ct);
            }
        }

        public async Task RollbackAsync(CancellationToken ct)
        {
            if (dbTransactionService != null)
            {
                await dbTransactionService.RollbackAsync(ct);
            }
        }
    }
}
