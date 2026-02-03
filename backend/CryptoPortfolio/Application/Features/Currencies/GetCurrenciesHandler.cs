// <copyright file="GetCurrenciesHandler.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Currenices;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Currencies
{
    internal class GetCurrenciesHandler(
        ICurrencyRepository currencyRepository,
        IValidator<GetCurrencies> validator,
        ILogger<GetCurrenciesHandler> logger)
        : AbstractHandler<GetCurrencies, IEnumerable<Currency>>(validator, logger)
    {
        protected override async Task<HandlerResponse<IEnumerable<Currency>>> HandleRequest(GetCurrencies request, CancellationToken cancellationToken)
        {
            var currencies = await currencyRepository.GetAllAsync(cancellationToken);
            return HandlerResponse<IEnumerable<Currency>>.Ok(currencies);
        }
    }
}
