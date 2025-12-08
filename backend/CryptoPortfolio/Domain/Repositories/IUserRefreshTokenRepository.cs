// <copyright file="IUserRefreshTokenRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Domain.Repositories
{
    /// <summary>
    /// User Refresh Token Repository Interface.
    /// </summary>
    public interface IUserRefreshTokenRepository : IDomainRepository
    {
        /// <summary>
        /// Create a new user refresh token.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the refresh token is being created.</param>
        /// <param name="token">The refresh token string.</param>
        /// <param name="expiresAt">The expiration date and time of the refresh token.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateAsync(int userId, string token, DateTime expiresAt, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve a user refresh token by its token string.
        /// </summary>
        /// <param name="token">The refresh token string to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user ID, active status, and expiration date of the token, or null if not found.</returns>
        Task<(int UserId, bool IsActive, DateTime ExpiresAt)?> GetAsync(string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Revoke a user refresh token.
        /// </summary>
        /// <param name="token">The refresh token string to revoke.</param>
        /// <param name="reason">The reason for revoking the token (optional).</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RevokeAsync(string token, string? reason = null, CancellationToken cancellationToken = default);
    }
}
