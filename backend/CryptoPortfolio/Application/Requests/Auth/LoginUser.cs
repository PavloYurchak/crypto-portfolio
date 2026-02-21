
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Response;

namespace CryptoPorfolio.Application.Requests.Auth
{
    public sealed record LoginUser(
    string Email,
    string Password) : IHandlerRequest<AuthResult>;
}
