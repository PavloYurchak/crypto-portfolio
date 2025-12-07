// <copyright file="UserRefreshTokenRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CryptoPorfolio.Infrastructure.Repositories
{
    internal sealed class UserRefreshTokenRepository(CryptoPorfolioContext context)
    : IUserRefreshTokenRepository
    {
        public async Task CreateAsync(
            int userId,
            string token,
            DateTime expiresAt,
            CancellationToken cancellationToken = default)
        {
            var entity = new Entities.UserRefreshToken
            {
                UserId = userId,
                Token = token,
                ExpiresAt = expiresAt,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await context.UserRefreshTokens.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<(int UserId, bool IsActive, DateTime ExpiresAt)?>
            GetAsync(string token, CancellationToken cancellationToken = default)
        {
            var entity = await context.UserRefreshTokens
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Token == token, cancellationToken);

            if (entity is null)
            {
                return null;
            }

            return (entity.UserId, entity.IsActive && entity.RevokedAt == null, entity.ExpiresAt);
        }

        public async Task RevokeAsync(
            string token,
            string? reason = null,
            CancellationToken cancellationToken = default)
        {
            var entity = await context.UserRefreshTokens
                .SingleOrDefaultAsync(e => e.Token == token, cancellationToken);

            if (entity is null)
            {
                return;
            }

            entity.IsActive = false;
            entity.RevokedAt = DateTime.UtcNow;
            entity.ReasonRevoked = reason;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
