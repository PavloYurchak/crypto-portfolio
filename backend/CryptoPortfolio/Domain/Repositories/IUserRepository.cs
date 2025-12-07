// <copyright file="IUserRepository.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    /// <summary>
    /// User Repository Interface.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A read-only collection of users.</returns>
        Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="model">The user model to create.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The created user.</returns>
        Task<User> CreateAsync(User model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new user with a password.
        /// </summary>
        /// <param name="model">The user model to create.</param>
        /// <param name="passwordHash">The password for the user.</param>
        /// <param name="passwordSalt">The salt used for hashing the password.</param>
        /// <param name="passwordAlgo">The algorithm used for hashing the password.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The created user.</returns>
        Task<User> CreateWithPasswordAsync(User model, string passwordHash, string passwordSalt, string passwordAlgo, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="model">The user model to update.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The updated user.</returns>
        Task<User> UpdateAsync(User model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft delete a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>True if the user was successfully soft deleted; otherwise, false.</returns>
        Task<bool> SoftDeleteAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get an internal user by their email address.
        /// </summary>
        /// <param name="email">The email address of the internal user.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The internal user if found; otherwise, null.</returns>
        Task<User?> GetInternalByEmailAsync(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update the password of a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="passwordHash">The new password hash.</param>
        /// <param name="passwordSalt">The salt used for hashing the password (optional).</param> 
        /// <param name="passwordAlgo">The algorithm used for hashing the password (optional).</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<User?> UpdatePasswordAsync(
            int userId,
            string passwordHash,
            string? passwordSalt,
            string? passwordAlgo,
            CancellationToken cancellationToken = default);
    }
}
