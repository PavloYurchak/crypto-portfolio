
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.TestAPI;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Status
{
    internal sealed class ApiStatusHandler(IValidator<ApiStatus> validator,
        ILogger<ApiStatusHandler> logger) : AbstractHandler<ApiStatus, bool>(validator, logger)
    {
        protected async override Task<HandlerResponse<bool>> HandleRequest(ApiStatus request, CancellationToken cancellationToken)
        {
            var result = await Task.FromResult(true);
            return HandlerResponse<bool>.Ok(result);
        }
    }
}
