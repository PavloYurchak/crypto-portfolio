
using CryptoPorfolio.Infrastructure.Context;
using CryptoPorfolio.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoPorfolio.Infrastructure.Abstraction
{
    internal class DatabaseInitializer<TDbContext>(TDbContext context) : IDatabaseInitializer
        where TDbContext : DbContext
    {
        public async Task InitializeAndSeed(CancellationToken cancellationToken = default)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync(cancellationToken);
            }
            else
            {
                await context.Database.EnsureCreatedAsync(cancellationToken);
            }

            await SeedCurrencies(cancellationToken);
            await SeedTransactionTypes(cancellationToken);
        }

        private async Task SeedCurrencies(CancellationToken cancellationToken)
        {
            if (context is not CryptoPorfolioContext cryptoContext)
            {
                return;
            }

            var exists = await cryptoContext.Currencies
                .AsNoTracking()
                .AnyAsync(c => c.Symbol == "USDT" && c.DeletedAt == null, cancellationToken);

            if (exists)
            {
                return;
            }

            cryptoContext.Currencies.Add(new Currency
            {
                Symbol = "USDT",
                Name = "USD",
                IsActive = true,
            });

            await cryptoContext.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedTransactionTypes(CancellationToken cancellationToken)
        {
            if (context is not CryptoPorfolioContext cryptoContext)
            {
                return;
            }

            var existingCodes = await cryptoContext.TransactionTypes
                .AsNoTracking()
                .Where(t => t.DeletedAt == null)
                .Select(t => t.Code)
                .ToListAsync(cancellationToken);

            var toAdd = new List<TransactionType>();

            if (!existingCodes.Contains("BUY"))
            {
                toAdd.Add(new TransactionType
                {
                    Code = "BUY",
                    Name = "Buy",
                    Description = "Buy transaction",
                    IsActive = true,
                });
            }

            if (!existingCodes.Contains("SELL"))
            {
                toAdd.Add(new TransactionType
                {
                    Code = "SELL",
                    Name = "Sell",
                    Description = "Sell transaction",
                    IsActive = true,
                });
            }

            if (toAdd.Count == 0)
            {
                return;
            }

            cryptoContext.TransactionTypes.AddRange(toAdd);
            await cryptoContext.SaveChangesAsync(cancellationToken);
        }
    }
}
