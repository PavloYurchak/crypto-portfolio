// <copyright file="ICurrencyRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    /// <summary>
    /// Repository interface for managing currencies.
    /// </summary>
    public interface ICurrencyRepository : IDomainRepository
    {
        /// <summary>
        /// Retrieves all currencies.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only collection of all currencies.</returns>
        Task<IReadOnlyCollection<Currency>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a currency by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the currency.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The currency with the specified identifier, or null if not found.</returns>
        Task<Currency?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a currency by its symbol.
        /// </summary>
        /// <param name="symbol">The symbol of the currency (e.g., BTC, ETH).</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The currency with the specified symbol, or null if not found.</returns>
        Task<Currency?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken = default);
    }
}
