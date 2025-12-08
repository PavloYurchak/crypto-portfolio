// <copyright file="AbstractHandler.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Abstractions.Messaging
{
    public abstract class AbstractHandler<TRequest, TResponse>(
        IValidator<TRequest>? validator,
        ILogger? logger) : IHandler<TRequest, TResponse>
    where TRequest : IHandlerRequest<TResponse>
    {
        public async Task<HandlerResponse<TResponse>> Handle(TRequest request, CancellationToken ct)
        {
            if (validator is not null)
            {
                var result = await validator.ValidateAsync(request, ct);
                if (!result.IsValid)
                {
                    var error = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
                    return HandlerResponse<TResponse>.BadRequest(error);
                }
            }

            try
            {
                return await this.HandleRequest(request, ct);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Handler {Handler} failed", this.GetType().Name);
                return HandlerResponse<TResponse>.Fail($"Unexpected error {ex.Message} | {ex.StackTrace}");
            }
        }

        protected abstract Task<HandlerResponse<TResponse>> HandleRequest(TRequest request, CancellationToken ct);
    }
}
