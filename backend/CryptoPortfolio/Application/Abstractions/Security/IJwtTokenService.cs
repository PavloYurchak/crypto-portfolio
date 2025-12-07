// <copyright file="IJwtTokenService.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Abstractions.Security
{
    /// <summary>
    /// JWT Token Service Interface
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates the access token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the access token is generated.</param>
        /// <returns>A tuple containing the generated token and its expiration date.</returns>
        (string Token, DateTime ExpiresAt) GenerateAccessToken(User user);

        /// <summary>
        /// Generates a new refresh token.
        /// </summary>
        /// <returns>The generated refresh token.</returns>
        string GenerateRefreshToken();
    }
}
