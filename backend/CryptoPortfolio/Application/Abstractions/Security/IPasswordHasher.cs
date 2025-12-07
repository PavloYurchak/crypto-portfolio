// <copyright file="IPasswordHasher.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Application.Abstractions.Security
{
    /// <summary>
    /// Password hasher interface.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes the password using the specified algorithm and generates a salt.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <param name="salt">The generated salt used in the hashing process.</param>
        /// <param name="algorithm">The hashing algorithm used.</param>
        /// <returns>The hashed password.</returns>
        string HashPassword(string password, out string? salt, out string? algorithm);

        /// <summary>
        /// Verifies if the provided password matches the hashed password.
        /// </summary>
        /// <param name="password">The plain text password to verify.</param>
        /// <param name="hash">The hashed password to compare against.</param>
        /// <param name="salt">The salt used during the hashing process.</param>
        /// <param name="algorithm">The hashing algorithm used.</param>
        /// <returns>True if the password matches the hash; otherwise, false.</returns>
        bool VerifyPassword(string password, string hash, string? salt, string? algorithm);
    }
}
