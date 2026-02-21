
using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.Currenices
{
    public record DeleteCurrency(int Id) : IHandlerRequest<bool>;
}
