using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.UserAssets;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.UserAssets
{
    internal sealed class GetUserAssetsHandler(
        IUserAssetRepository userAssetRepository,
        IValidator<GetUserAssets> validator,
        ILogger<GetUserAssetsHandler> logger)
        : AbstractHandler<GetUserAssets, IEnumerable<UserAsset>>(validator, logger)
    {
        protected override async Task<HandlerResponse<IEnumerable<UserAsset>>> HandleRequest(
            GetUserAssets request,
            CancellationToken cancellationToken)
        {
            var assets = await userAssetRepository.GetByUserAsync(request.UserId, cancellationToken);
            return HandlerResponse<IEnumerable<UserAsset>>.Ok(assets);
        }
    }
}
