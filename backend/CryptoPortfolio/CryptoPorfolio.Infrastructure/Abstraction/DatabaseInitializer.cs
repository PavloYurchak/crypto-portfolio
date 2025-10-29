// <copyright file="DatabaseInitializer.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

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
        }
    }
}
