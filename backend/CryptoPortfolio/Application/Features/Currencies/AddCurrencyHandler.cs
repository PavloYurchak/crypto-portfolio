
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Mapping;
using CryptoPorfolio.Application.Requests.Currenices;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Currencies
{
    internal class AddCurrencyHandler(
        ICurrencyRepository currencyRepository,
        IDbTransactionService dbTransactionService,
        IValidator<AddCurrency> validator,
        ILogger<AddCurrencyHandler> logger)
        : AbstractHandler<AddCurrency, Currency>(validator, logger)
    {
        protected override async Task<HandlerResponse<Currency>> HandleRequest(AddCurrency request, CancellationToken cancellationToken)
        {
            await dbTransactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                var addedCurrency = await currencyRepository.CreateAsync(request.ToModel(), cancellationToken);

                await dbTransactionService.CommitAsync(cancellationToken);
                return addedCurrency != null
                    ? HandlerResponse<Currency>.Ok(addedCurrency)
                    : HandlerResponse<Currency>.Fail("Failed to add currency.");
            }
            catch (Exception)
            {
                await dbTransactionService.RollbackAsync(cancellationToken);
                logger.LogError("Failed to add currency with Name {CurrencyName}", request.Name);
                return HandlerResponse<Currency>.Fail("Failed to add currency.");
            }
        }
    }
}
