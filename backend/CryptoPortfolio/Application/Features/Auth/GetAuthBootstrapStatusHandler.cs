using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Auth;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Auth
{
    internal sealed class GetAuthBootstrapStatusHandler(
        IValidator<GetAuthBootstrapStatus> validator,
        ILogger<GetAuthBootstrapStatusHandler> logger,
        IUserRepository userRepository)
        : AbstractHandler<GetAuthBootstrapStatus, bool>(validator, logger)
    {
        protected override async Task<HandlerResponse<bool>> HandleRequest(
            GetAuthBootstrapStatus request,
            CancellationToken cancellationToken)
        {
            var hasActiveAdmin = await userRepository.HasActiveAdminAsync(cancellationToken);
            return HandlerResponse<bool>.Ok(hasActiveAdmin);
        }
    }
}
