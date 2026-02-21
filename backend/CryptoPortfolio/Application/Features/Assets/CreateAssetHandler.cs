
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
    internal class CreateAssetHandler(
        IAssetRepository assetRepository,
        IDbTransactionService dbTransactionService,
        IValidator<AddAsset> validator,
        ILogger<CreateAssetHandler> logger)
        : AbstractHandler<AddAsset, Asset>(validator, logger)
    {
        protected override async Task<HandlerResponse<Asset>> HandleRequest(AddAsset request, CancellationToken cancellationToken)
        {
            await dbTransactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                var addedAsset = await assetRepository.CreateAssetAsync(
                    request.ToModel(),
                    cancellationToken);

                await dbTransactionService.CommitAsync(cancellationToken);

                return addedAsset != null
                    ? HandlerResponse<Asset>.Ok(addedAsset)
                    : HandlerResponse<Asset>.Fail("Failed to create asset.");
            }
            catch (Exception)
            {
                await dbTransactionService.RollbackAsync(cancellationToken);
                logger.LogError("Failed to create asset with Name {AssetName}", request.Name);
                return HandlerResponse<Asset>.Fail("Failed to create asset.");
            }
        }
    }
}
