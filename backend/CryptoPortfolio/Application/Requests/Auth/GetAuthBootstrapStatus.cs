using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.Auth
{
    public sealed record GetAuthBootstrapStatus() : IHandlerRequest<bool>;
}
