
using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.Currenices
{
    public record GetCurrencies : IHandlerRequest<IEnumerable<Domain.Models.Currency>>;
}
