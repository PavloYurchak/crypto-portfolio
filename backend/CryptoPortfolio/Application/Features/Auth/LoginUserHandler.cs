// <copyright file="LoginUserHandler.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Abstractions.Security;
using CryptoPorfolio.Application.Mapping;
using CryptoPorfolio.Application.Requests.Auth;
using CryptoPorfolio.Application.Response;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Auth
{
    internal sealed class LoginUserHandler(
    IValidator<LoginUser> validator,
    ILogger<LoginUserHandler> logger,
    IUserRepository userRepository,
    IUserRefreshTokenRepository refreshTokenRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtTokenService)
    : AbstractHandler<LoginUser, AuthResult>(validator, logger)
    {
        protected override async Task<HandlerResponse<AuthResult>> HandleRequest(
            LoginUser request,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(
                request.Email,
                cancellationToken);

            if (user is null)
            {
                return HandlerResponse<AuthResult>.Fail("Invalid email or password.");
            }

            if (!user.IsActive)
            {
                return HandlerResponse<AuthResult>.Fail("Invalid email or password.");
            }

            var valid = passwordHasher.VerifyPassword(
                request.Password,
                user.PasswordHash,
                user.PasswordSalt,
                user.PasswordAlgo);

            if (!valid)
            {
                return HandlerResponse<AuthResult>.Fail("Invalid email or password.");
            }

            var (accessToken, expiresAt) = jwtTokenService.GenerateAccessToken(user);
            var refreshToken = jwtTokenService.GenerateRefreshToken();
            var refreshExpiresAt = DateTime.UtcNow.AddDays(7);

            await refreshTokenRepository.CreateAsync(
                user.Id,
                refreshToken,
                refreshExpiresAt,
                cancellationToken);

            return HandlerResponse<AuthResult>.Ok(user.ToAuthResult(accessToken, expiresAt, refreshToken));
        }
    }
}
