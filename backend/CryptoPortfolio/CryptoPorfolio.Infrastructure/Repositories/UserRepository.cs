// <copyright file="UserRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Infrastructure.Context;
using CryptoPorfolio.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CryptoPorfolio.Infrastructure.Repositories
{
    internal sealed class UserRepository(CryptoPorfolioContext context)
    : IUserRepository
    {
        public async Task<IReadOnlyCollection<User>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await context.Users
                .AsNoTracking()
                .Where(e => e.DeletedAt == null && e.IsActive)
                .OrderBy(e => e.Id)
                .Select(e => e.ToModel())
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var entity = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id && e.DeletedAt == null, cancellationToken);

            return entity?.ToModel();
        }

        public async Task<User?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            var entity = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(
                    e =>
                    e.Email == email &&
                    e.DeletedAt == null,
                    cancellationToken);

            return entity?.ToModel();
        }

        public async Task<User> CreateAsync(
            User model,
            CancellationToken cancellationToken = default)
        {
            var entity = model.ToEntity();
            entity.CreatedAt = DateTime.UtcNow;

            await context.Users.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            entity = await context.Users
                .AsNoTracking()
                .SingleAsync(e => e.Id == entity.Id, cancellationToken);

            return entity.ToModel();
        }

        public async Task<User> UpdateAsync(
            User model,
            CancellationToken cancellationToken = default)
        {
            var entity = await context.Users
                .SingleOrDefaultAsync(
                    e =>
                    e.Id == model.Id &&
                    e.DeletedAt == null,
                    cancellationToken);

            if (entity is not null)
            {
                entity.Email = model.Email;
                entity.UserName = model.UserName;
                entity.EmailConfirmed = model.EmailConfirmed;
                entity.IsLockedOut = model.IsLockedOut;
                entity.LockoutEndAt = model.LockoutEndAt;
                entity.FailedAccessCount = model.FailedAccessCount;
                entity.TwoFactorEnabled = model.TwoFactorEnabled;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.IsActive = model.IsActive;

                await context.SaveChangesAsync(cancellationToken);
            }

            return entity.ToModel();
        }

        public async Task<bool> SoftDeleteAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var entity = await context.Users
                .SingleOrDefaultAsync(
                    e =>
                    e.Id == id &&
                    e.DeletedAt == null,
                    cancellationToken);

            if (entity is null)
            {
                return false;
            }

            entity.DeletedAt = DateTime.UtcNow;
            entity.IsActive = false;

            await context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<User> CreateWithPasswordAsync(User model, string passwordHash, string passwordSalt, string passwordAlgo, CancellationToken cancellationToken = default)
        {
            var entity = model.ToEntity();

            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;
            entity.PasswordAlgo = passwordAlgo;
            entity.CreatedAt = DateTime.UtcNow;

            await context.Users.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            entity = await context.Users
                .AsNoTracking()
                .SingleAsync(e => e.Id == entity.Id, cancellationToken);

            return entity.ToModel();
        }

        public async Task<(User User, string PasswordHash, string? PasswordSalt, string? PasswordAlgo)?>
        GetWithPasswordByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var entity = await context.Users
                .SingleOrDefaultAsync(
                    e =>
                    e.Email == email &&
                    e.DeletedAt == null,
                    cancellationToken);

            if (entity is null)
            {
                return null;
            }

            var model = entity.ToModel();
            return (model, entity.PasswordHash, entity.PasswordSalt, entity.PasswordAlgo);
        }

        public async Task<User?> UpdatePasswordAsync(
            int userId,
            string passwordHash,
            string? passwordSalt,
            string? passwordAlgo,
            CancellationToken cancellationToken = default)
        {
            var entity = await context.Users
                .SingleOrDefaultAsync(
                    e =>
                    e.Id == userId &&
                    e.DeletedAt == null,
                    cancellationToken);

            if (entity is not null)
            {
                entity.PasswordHash = passwordHash;
                entity.PasswordSalt = passwordSalt;
                entity.PasswordAlgo = passwordAlgo;
                entity.UpdatedAt = DateTime.UtcNow;

                await context.SaveChangesAsync(cancellationToken);
            }

            return entity.ToModel();
        }

        public async Task<User?> GetInternalByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var entity = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(
                e =>
                e.Email == email &&
                e.DeletedAt == null &&
                e.IsActive,
                cancellationToken);

            return entity?.ToModel();
        }
    }
}
