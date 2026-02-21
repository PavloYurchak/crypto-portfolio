using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Response;

namespace CryptoPorfolio.Application.Requests.Auth
{
    public sealed record RefreshUserToken(string RefreshToken) : IHandlerRequest<AuthResult>;
}
