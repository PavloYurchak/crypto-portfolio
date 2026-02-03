// <copyright file="DeleteCurrency.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.Currenices
{
    public record DeleteCurrency(int Id) : IHandlerRequest<bool>;
}
