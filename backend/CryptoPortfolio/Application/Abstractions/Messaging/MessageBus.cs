// <copyright file="MessageBus.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace CryptoPorfolio.Application.Abstractions.Messaging
{
    internal sealed class MessageBus : ISender
    {
        private readonly IServiceProvider _sp;

        public MessageBus(IServiceProvider sp) => _sp = sp;

        public Task<HandlerResponse<TResponse>> Send<TResponse>(
            IHandlerRequest<TResponse> request, CancellationToken ct = default)
        {
            var handlerType = typeof(IHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _sp.GetRequiredService(handlerType);

            return (Task<HandlerResponse<TResponse>>)handlerType
                .GetMethod(nameof(IHandler<IHandlerRequest<TResponse>, TResponse>.Handle))!
                .Invoke(handler, new object?[] { request, ct })!;
        }
    }
}
