// <copyright file="GetCurrencies.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.Currenices
{
    public record GetCurrencies : IHandlerRequest<IEnumerable<Domain.Models.Currency>>;
}
