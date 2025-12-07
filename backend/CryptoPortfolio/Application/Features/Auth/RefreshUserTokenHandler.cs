// <copyright file="RefreshUserTokenHandler.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Abstractions.Security;
using CryptoPorfolio.Application.Requests.Auth;
using CryptoPorfolio.Application.Response;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Auth
{
    internal sealed class RefreshUserTokenHandler(
    IValidator<RefreshUserToken> validator,
    ILogger<RefreshUserTokenHandler> logger,
    IUserRepository userRepository,
    IUserRefreshTokenRepository refreshTokenRepository,
    IJwtTokenService jwtTokenService)
    : AbstractHandler<RefreshUserToken, AuthResult>(validator, logger)
    {
        protected override async Task<HandlerResponse<AuthResult>> HandleRequest(
            RefreshUserToken request,
            CancellationToken cancellationToken)
        {
            var stored = await refreshTokenRepository.GetAsync(request.RefreshToken, cancellationToken);
            if (stored is null || !stored.Value.IsActive || stored.Value.ExpiresAt <= DateTime.UtcNow)
            {
                return HandlerResponse<AuthResult>.Fail("Invalid or expired refresh token.");
            }

            var user = await userRepository.GetByIdAsync(stored.Value.UserId, cancellationToken);
            if (user is null)
            {
                return HandlerResponse<AuthResult>.Fail("User not found.");
            }

            // Можна відкликати старий refresh токен
            await refreshTokenRepository.RevokeAsync(request.RefreshToken, "Replaced by new token", cancellationToken);

            var (accessToken, expiresAt) = jwtTokenService.GenerateAccessToken(user);
            var newRefreshToken = jwtTokenService.GenerateRefreshToken();
            var refreshExpiresAt = DateTime.UtcNow.AddDays(7);

            await refreshTokenRepository.CreateAsync(user.Id, newRefreshToken, refreshExpiresAt, cancellationToken);

            var result = new AuthResult
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                AccessToken = accessToken,
                AccessTokenExpiresAt = expiresAt,
                RefreshToken = newRefreshToken,
            };

            return HandlerResponse<AuthResult>.Ok(result);
        }
    }
}
