
using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.Assets
{
    public sealed record DeleteAsset(int Id) : IHandlerRequest<bool>;
}
