
using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.Currenices
{
    public record UpdateCurrency(int Id) : IHandlerRequest<Domain.Models.Currency>
    {
        public required string Name { get; init; }

        public required string Symbol { get; init; }
    }
}
