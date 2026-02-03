// <copyright file="DeleteCurrencyHandler.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Currenices;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Currencies
{
    internal class DeleteCurrencyHandler(
        ICurrencyRepository currencyRepository,
        IValidator<DeleteCurrency> validator,
        ILogger<DeleteCurrencyHandler> logger)
        : AbstractHandler<DeleteCurrency, bool>(validator, logger)
    {
        protected override async Task<HandlerResponse<bool>> HandleRequest(DeleteCurrency request, CancellationToken cancellationToken)
        {
            var currency = await currencyRepository.GetByIdAsync(request.Id, cancellationToken);

            if (currency == null)
            {
                return HandlerResponse<bool>.NotFound("Currency not found.");
            }

            var deleted = await currencyRepository.DeleteAsync(request.Id, cancellationToken);
            return deleted
                ? HandlerResponse<bool>.Ok(deleted)
                : HandlerResponse<bool>.NotFound("Currency not found.");
        }
    }
}
