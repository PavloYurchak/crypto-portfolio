// <copyright file="IDatabaseInitializer.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Infrastructure.Abstraction
{
    /// <summary>
    /// Database Initializer interface.
    /// </summary>
    public interface IDatabaseInitializer
    {
        /// <summary>
        /// Initializes and seeds the database.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task InitializeAndSeed(CancellationToken cancellationToken = default(CancellationToken));
    }
}
