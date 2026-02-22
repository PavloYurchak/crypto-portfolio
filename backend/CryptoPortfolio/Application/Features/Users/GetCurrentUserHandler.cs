using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Mapping;
using CryptoPorfolio.Application.Requests.Users;
using CryptoPorfolio.Application.Response;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Users
{
    internal sealed class GetCurrentUserHandler(
        IUserRepository userRepository,
        IValidator<GetCurrentUser> validator,
        ILogger<GetCurrentUserHandler> logger)
        : AbstractHandler<GetCurrentUser, UserResponse>(validator, logger)
    {
        protected override async Task<HandlerResponse<UserResponse>> HandleRequest(
            GetCurrentUser request,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user is null)
            {
                return HandlerResponse<UserResponse>.NotFound("User not found.");
            }

            return HandlerResponse<UserResponse>.Ok(user.ToUserResponse());
        }
    }
}
