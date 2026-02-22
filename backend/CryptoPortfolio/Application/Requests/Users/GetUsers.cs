using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Response;

namespace CryptoPorfolio.Application.Requests.Users
{
    public sealed record GetUsers() : IHandlerRequest<IEnumerable<UserResponse>>;
}
