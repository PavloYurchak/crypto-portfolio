// <copyright file="AuthResultMappingProfile.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Response;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Mapping
{
    internal static class AuthResultMappingProfile
    {
        public static AuthResult ToAuthResult(
            this User entity,
            string accessToken,
            DateTime accessTokenExpiresAt,
            string refreshToken)
        {
            return new AuthResult
            {
                UserId = entity.Id,
                Email = entity.Email,
                UserName = entity.UserName,
                AccessToken = accessToken,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshToken = refreshToken,
            };
        }
    }
}
