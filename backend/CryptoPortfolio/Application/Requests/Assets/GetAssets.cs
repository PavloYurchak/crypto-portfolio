
using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.Assets
{
    public sealed record GetAssets() : IHandlerRequest<IEnumerable<Domain.Models.Asset>>;
}
