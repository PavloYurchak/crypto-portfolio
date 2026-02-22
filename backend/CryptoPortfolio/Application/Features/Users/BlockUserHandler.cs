using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Mapping;
using CryptoPorfolio.Application.Requests.Users;
using CryptoPorfolio.Application.Response;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Users
{
    internal sealed class BlockUserHandler(
        IUserRepository userRepository,
        IValidator<BlockUser> validator,
        ILogger<BlockUserHandler> logger)
        : AbstractHandler<BlockUser, UserResponse>(validator, logger)
    {
        protected override async Task<HandlerResponse<UserResponse>> HandleRequest(
            BlockUser request,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user is null)
            {
                return HandlerResponse<UserResponse>.NotFound("User not found.");
            }

            user.IsActive = false;
            user.IsLockedOut = true;
            user.LockoutEndAt = DateTime.UtcNow;

            var updated = await userRepository.UpdateAsync(user, cancellationToken);

            return HandlerResponse<UserResponse>.Ok(updated.ToUserResponse());
        }
    }
}
