
using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Mapping;
using CryptoPorfolio.Application.Requests.Assets;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Assets
{
    internal class UpdateAssetHandler(IAssetRepository assetRepository,
        IDbTransactionService dbTransactionService,
        IValidator<UpdateAsset> validator,
        ILogger<UpdateAssetHandler> logger)
        : AbstractHandler<UpdateAsset, Asset>(validator, logger)
    {
        protected override async Task<HandlerResponse<Asset>> HandleRequest(UpdateAsset request, CancellationToken cancellationToken)
        {
            var asset = await assetRepository.GetByIdAsync(request.Id, cancellationToken);

            if (asset == null)
            {
                await dbTransactionService.RollbackAsync(cancellationToken);
                return HandlerResponse<Asset>.NotFound("Asset not found.");
            }

            await dbTransactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await assetRepository.UpdateAssetAsync(request.ToModel(), cancellationToken);

                await dbTransactionService.CommitAsync(cancellationToken);

                return result != null
                    ? HandlerResponse<Asset>.Ok(result!)
                    : HandlerResponse<Asset>.NotFound("Failed to update asset.");
            }
            catch (Exception)
            {
                await dbTransactionService.RollbackAsync(cancellationToken);
                logger.LogError("Failed to update asset with Id {AssetId}", request.Id);
                return HandlerResponse<Asset>.Fail("Failed to update asset.");
            }
        }
    }
}
