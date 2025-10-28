// <copyright file="IHandlerRequest.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Application.Abstractions.Messaging
{
    /// <summary>
    /// Represents a handler request.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response returned by the handler.</typeparam>
    public interface IHandlerRequest<TResponse>;
}
