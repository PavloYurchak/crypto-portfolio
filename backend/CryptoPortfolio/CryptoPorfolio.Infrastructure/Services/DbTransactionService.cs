// <copyright file="DbTransactionService.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

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
            this.dbTransactionService = await context.Database.BeginTransactionAsync(ct);
        }

        public async Task CommitAsync(CancellationToken ct)
        {
            if (this.dbTransactionService != null)
            {
                await this.dbTransactionService.CommitAsync(ct);
            }
        }

        public async Task RollbackAsync(CancellationToken ct)
        {
            if (this.dbTransactionService != null)
            {
                await this.dbTransactionService.RollbackAsync(ct);
            }
        }
    }
}
