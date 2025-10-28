// <copyright file="ApiStatus.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.TestAPI
{
    public sealed record ApiStatus() : IHandlerRequest<bool>;
}
