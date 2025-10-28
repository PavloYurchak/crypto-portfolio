// <copyright file="ISender.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Application.Abstractions.Messaging
{
    /// <summary>
    /// Sends requests to their corresponding handlers.
    /// </summary>
    public interface ISender
    {
        /// <summary>
        /// Sends a request to its corresponding handler and returns the response.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response returned by the handler.</typeparam>
        /// <param name="request">The request to be sent to the handler.</param>
        /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the handler's response.</returns>
        Task<HandlerResponse<TResponse>> Send<TResponse>(
            IHandlerRequest<TResponse> request,
            CancellationToken ct = default);
    }
}
