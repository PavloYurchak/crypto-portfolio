// <copyright file="IHandler.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Application.Abstractions.Messaging
{
    /// <summary>
    /// Represents a handler for a specific request type.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IHandler<in TRequest, TResponse>
    where TRequest : IHandlerRequest<TResponse>
    {
        /// <summary>
        /// Handles the specified request and returns a response.
        /// </summary>
        /// <param name="request">The request to be handled.</param>
        /// <param name="ct">The cancellation token to observe.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the handler response.</returns>
        Task<HandlerResponse<TResponse>> Handle(TRequest request, CancellationToken ct);
    }
}
