
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Assets;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Assets
{
    internal class DeleteAssetHandler(
        IAssetRepository assetRepository,
        IValidator<DeleteAsset> validator,
        ILogger<DeleteAssetHandler> logger)
        : AbstractHandler<DeleteAsset, bool>(validator, logger)
    {
        protected override async Task<HandlerResponse<bool>> HandleRequest(DeleteAsset request, CancellationToken cancellationToken)
        {
            var asset = await assetRepository.GetByIdAsync(request.Id, cancellationToken);

            if (asset == null)
            {
                return HandlerResponse<bool>.NotFound("Asset not found.");
            }

            var deleted = await assetRepository.DeleteAssetAsync(request.Id, cancellationToken);

            return deleted
                ? HandlerResponse<bool>.Ok(deleted)
                : HandlerResponse<bool>.NotFound("Asset not found.");
        }
    }
}
