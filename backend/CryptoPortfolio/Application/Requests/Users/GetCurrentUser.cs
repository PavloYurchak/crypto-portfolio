using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Response;

namespace CryptoPorfolio.Application.Requests.Users
{
    public sealed record GetCurrentUser(int UserId) : IHandlerRequest<UserResponse>;
}
