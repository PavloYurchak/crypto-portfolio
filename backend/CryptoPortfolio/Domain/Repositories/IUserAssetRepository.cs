// <copyright file="IUserAssetRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    /// <summary>
    /// User asset repository interface.
    /// </summary>
    public interface IUserAssetRepository
    {
        /// <summary>
        /// Get user assets by user id.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only collection of user assets.</returns>
        Task<IReadOnlyCollection<UserAsset>> GetByUserAsync(int userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a specific user asset by user id and asset id.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="assetId">The unique identifier of the asset.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The user asset if found; otherwise, null.</returns>
        Task<UserAsset?> GetByUserAndAssetAsync(int userId, int assetId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Insert or update a user asset.
        /// </summary>
        /// <param name="model">The user asset model to upsert.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The upserted user asset.</returns>
        Task<UserAsset?> Create(UserAsset model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a user asset.
        /// </summary>
        /// <param name="model">The user asset model to update.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The updated user asset.</returns>
        Task<UserAsset?> Update(UserAsset model, CancellationToken cancellationToken = default);
    }
}
