// <copyright file="IDbTransaction.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Domain.Services
{
    /// <summary>
    /// Represents a database transaction.
    /// </summary>
    public interface IDbTransactionService
    {
        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task BeginTransactionAsync(CancellationToken ct);

        /// <summary>
        /// Commits the current database transaction.
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CommitAsync(CancellationToken ct);

        /// <summary>
        /// Rolls back the current database transaction.
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RollbackAsync(CancellationToken ct);
    }
}
