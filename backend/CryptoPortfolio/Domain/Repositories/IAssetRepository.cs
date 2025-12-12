// <copyright file="IAssetRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    /// <summary>
    /// Repository interface for managing assets.
    /// </summary>
    public interface IAssetRepository : IDomainRepository
    {
        /// <summary>
        /// Get all assets.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only collection of all assets.</returns>
        Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get asset by identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the asset.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The asset with the specified identifier, or null if not found.</returns>
        Task<Asset?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get asset by symbol.
        /// </summary>
        /// <param name="symbol">The symbol representing the asset.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The asset with the specified symbol, or null if not found.</returns>
        Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new asset.
        /// </summary>
        /// <param name="asset">The asset to be created.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The created asset, or null if creation failed.</returns>
        Task<Asset?> CreateAssetAsync(Asset model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update an existing asset.
        /// </summary>
        /// <param name="asset">The asset with updated information.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The updated asset, or null if the update failed.</returns>
        Task<Asset?> UpdateAssetAsync(Asset model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete an asset by identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the asset to be deleted.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>True if the asset was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteAssetAsync(int id, CancellationToken cancellationToken = default);
    }
}
