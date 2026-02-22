using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Mapping;
using CryptoPorfolio.Application.Requests.Users;
using CryptoPorfolio.Application.Response;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Users
{
    internal sealed class GetUsersHandler(
        IUserRepository userRepository,
        IValidator<GetUsers> validator,
        ILogger<GetUsersHandler> logger)
        : AbstractHandler<GetUsers, IEnumerable<UserResponse>>(validator, logger)
    {
        protected override async Task<HandlerResponse<IEnumerable<UserResponse>>> HandleRequest(
            GetUsers request,
            CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAllIncludingInactiveAsync(cancellationToken);
            var result = users.Select(u => u.ToUserResponse()).ToList();
            return HandlerResponse<IEnumerable<UserResponse>>.Ok(result);
        }
    }
}
