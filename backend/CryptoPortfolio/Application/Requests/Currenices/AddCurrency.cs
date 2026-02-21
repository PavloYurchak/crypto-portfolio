
using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.Currenices
{
    public record AddCurrency : IHandlerRequest<Domain.Models.Currency>
    {
        public required string Name { get; init; }

        public required string Symbol { get; init; }
    }
}
