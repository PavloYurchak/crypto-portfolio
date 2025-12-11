// <copyright file="RegisterUserHandler.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Abstractions.Security;
using CryptoPorfolio.Application.Mapping;
using CryptoPorfolio.Application.Models;
using CryptoPorfolio.Application.Requests.Auth;
using CryptoPorfolio.Application.Response;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Auth
{
    internal sealed class RegisterUserHandler(
    IValidator<RegisterUser> validator,
    ILogger<RegisterUserHandler> logger,
    IUserRepository userRepository,
    IUserRefreshTokenRepository refreshTokenRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtTokenService)
    : AbstractHandler<RegisterUser, AuthResult>(validator, logger)
    {
        protected override async Task<HandlerResponse<AuthResult>> HandleRequest(
            RegisterUser request,
            CancellationToken cancellationToken)
        {
            var isUserEmpty = await userRepository.IsUserEmptyAsync(cancellationToken);

            var existingUser = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser is not null)
            {
                return HandlerResponse<AuthResult>.Fail("User with this email already exists.");
            }

            var hash = passwordHasher.HashPassword(request.Password, out var salt, out var algo);

            var userModel = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                PasswordHash = hash,
                EmailConfirmed = false,
                IsLockedOut = false,
                FailedAccessCount = 0,
                TwoFactorEnabled = false,
                IsActive = true,
            };

            if (isUserEmpty)
            {
                userModel.UserName = "Admin";
                userModel.UserType = UserTypes.Admin;
            }

            var createdUser = await userRepository.CreateWithPasswordAsync(
                userModel,
                hash,
                salt ?? throw new ArgumentException(),
                algo ?? throw new ArgumentException(),
                cancellationToken);

            var (accessToken, expiresAt) = jwtTokenService.GenerateAccessToken(createdUser);

            var refreshToken = jwtTokenService.GenerateRefreshToken();
            var refreshExpiresAt = DateTime.UtcNow.AddDays(7); // або з конфігурації

            await refreshTokenRepository.CreateAsync(createdUser.Id, refreshToken, refreshExpiresAt, cancellationToken);

            return HandlerResponse<AuthResult>.Ok(createdUser.ToAuthResult(accessToken, expiresAt, refreshToken));
        }
    }
}
