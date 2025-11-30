// <copyright file="TransactionTypeRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Infrastructure.Context;
using CryptoPorfolio.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CryptoPorfolio.Infrastructure.Repositories
{
    internal sealed class TransactionTypeRepository(CryptoPorfolioContext context) : ITransactionTypeRepository
    {
        public async Task<IReadOnlyCollection<TransactionTypeModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.TransactionTypes
                .AsNoTracking()
                .Where(e => e.DeletedAt == null && e.IsActive)
                .OrderBy(e => e.Code)
                .Select(e => e.ToModel())
                .ToListAsync(cancellationToken);
        }

        public async Task<TransactionTypeModel?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            var entity = await context.TransactionTypes
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Code == code && e.DeletedAt == null, cancellationToken);

            return entity?.ToModel();
        }
    }
}
