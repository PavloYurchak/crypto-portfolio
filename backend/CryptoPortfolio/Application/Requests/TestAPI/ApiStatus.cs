
using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.TestAPI
{
    public sealed record ApiStatus() : IHandlerRequest<bool>;
}
