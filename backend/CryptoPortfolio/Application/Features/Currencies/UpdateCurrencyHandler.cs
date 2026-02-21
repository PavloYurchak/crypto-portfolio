
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
    internal class UpdateCurrencyHandler(
        ICurrencyRepository currencyRepository,
        IDbTransactionService dbTransactionService,
        IValidator<UpdateCurrency> validator,
        ILogger<UpdateCurrencyHandler> logger)
        : AbstractHandler<UpdateCurrency, Currency>(validator, logger)
    {
        protected override async Task<HandlerResponse<Currency>> HandleRequest(UpdateCurrency request, CancellationToken cancellationToken)
        {
            var currency = currencyRepository.GetByIdAsync(request.Id, cancellationToken);

            if (currency == null)
            {
                return HandlerResponse<Currency>.NotFound("Currency not found.");
            }

            await dbTransactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                var updatedCurrency = await currencyRepository.UpdateAsync(request.ToModel(), cancellationToken);
                await dbTransactionService.CommitAsync(cancellationToken);
                return updatedCurrency != null
                    ? HandlerResponse<Currency>.Ok(updatedCurrency)
                    : HandlerResponse<Currency>.Fail("Failed to update currency.");
            }
            catch (Exception)
            {
                await dbTransactionService.RollbackAsync(cancellationToken);
                logger.LogError("Failed to update currency with Id {CurrencyId}", request.Id);
                return HandlerResponse<Currency>.Fail("Failed to update currency.");
            }
        }
    }
}
