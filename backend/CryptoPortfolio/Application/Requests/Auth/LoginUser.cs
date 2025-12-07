// <copyright file="LoginUser.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Response;

namespace CryptoPorfolio.Application.Requests.Auth
{
    public sealed record LoginUser(
    string Email,
    string Password) : IHandlerRequest<AuthResult>;
}
