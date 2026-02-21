
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Response;

namespace CryptoPorfolio.Application.Requests.Auth
{
    public sealed record RegisterUser(
    string Email,
    string UserName,
    string Password) : IHandlerRequest<AuthResult>;
}
