// <copyright file="IHandlerResponse.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Application.Abstractions.Messaging
{
    /// <summary>
    /// Represents a handler response.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response result.</typeparam>
    public interface IHandlerResponse<TResponse>
    {
        /// <summary>
        /// Gets the status code of the handler response.
        /// </summary>
        int Code { get; }

        /// <summary>
        /// Gets the result of the handler response.
        /// </summary>
        TResponse Result { get; }

        /// <summary>
        /// Gets the errors of the handler response, if any.
        /// </summary>
        string[]? Errors { get; }
    }
}
