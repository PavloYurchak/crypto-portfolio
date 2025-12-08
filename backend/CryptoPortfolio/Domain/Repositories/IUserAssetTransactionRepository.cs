// <copyright file="IUserAssetTransactionRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    /// <summary>
    /// Repository interface for managing user asset transactions.
    /// </summary>
    public interface IUserAssetTransactionRepository : IDomainRepository
    {
        /// <summary>
        /// Gets all transactions for a specific user asset.
        /// </summary>
        /// <param name="userAssetId">The unique identifier of the user asset.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A collection of user asset transactions.</returns>
        Task<IEnumerable<UserAssetTransaction>> GetUserTransactionsAsync(
            int userAssetId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a user asset transaction by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The user asset transaction if found; otherwise, null.</returns>
        Task<UserAssetTransaction?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new user asset transaction.
        /// </summary>
        /// <param name="model">The user asset transaction to create.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The created user asset transaction.</returns>
        Task<UserAssetTransaction> CreateAsync(UserAssetTransaction model, CancellationToken cancellationToken = default);
    }
}
