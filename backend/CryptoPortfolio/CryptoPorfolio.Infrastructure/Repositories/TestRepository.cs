// <copyright file="TestRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Infrastructure.Context;
using CryptoPorfolio.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CryptoPorfolio.Infrastructure.Repositories
{
    public class TestRepository(CryptoPorfolioContext context) : ITestRepository
    {
        public async Task<Test?> CreateTest(Test test, CancellationToken cancellationToken = default)
        {
            var entity = test.ToEntity();
            entity.CreatedAt = DateTime.UtcNow;

            var result = await context.Tests.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return await this.GetTestById(result.Entity.Id, cancellationToken);
        }

        public async Task<bool> DeleteTest(IEnumerable<int> testIds, CancellationToken cancellationToken = default)
        {
            var deleted = await context.Tests
                .Where(e => testIds.Contains(e.Id) && !e.DeletedAt.HasValue)
                .ExecuteUpdateAsync(e => e.SetProperty(p => p.DeletedAt, p => DateTime.UtcNow), cancellationToken);
            return deleted == testIds.Count();
        }

        public async Task<Test?> GetTestById(int id, CancellationToken cancellationToken = default)
        {
            var result = await context.Tests
                .SingleOrDefaultAsync(e => e.Id == id && !e.DeletedAt.HasValue, cancellationToken);
            return result?.ToModel();
        }

        public async Task<IEnumerable<Test>> GetTests(CancellationToken cancellationToken = default)
        {
            var result = await context.Tests.ToListAsync(cancellationToken);
            return result.Select(e => e.ToModel());
        }

        public async Task<Test?> UpdateTest(Test test, CancellationToken cancellationToken = default)
        {
            var entity = await context.Tests
                .SingleOrDefaultAsync(e => e.Id == test.Id && !e.DeletedAt.HasValue, cancellationToken);
            if (entity != null)
            {
                entity.Name = test.Name;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.IsActive = test.IsActive;
                await context.SaveChangesAsync(cancellationToken);
            }

            return await this.GetTestById(test.Id, cancellationToken);
        }
    }
}
