// <copyright file="ITransactionTypeRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    /// <summary>
    /// Repository interface for managing transaction types.
    /// </summary>
    public interface ITransactionTypeRepository
    {
        /// <summary>
        /// Gets all transaction types.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only collection of transaction types.</returns>
        Task<IReadOnlyCollection<TransactionTypeModel>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a transaction type by its code.
        /// </summary>
        /// <param name="code">The code of the transaction type.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The transaction type with the specified code, or null if not found.</returns>
        Task<TransactionTypeModel?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
