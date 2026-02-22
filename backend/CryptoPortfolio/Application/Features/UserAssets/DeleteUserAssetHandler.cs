using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.UserAssets;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.UserAssets
{
    internal sealed class DeleteUserAssetHandler(
        IUserAssetRepository userAssetRepository,
        IValidator<DeleteUserAsset> validator,
        ILogger<DeleteUserAssetHandler> logger)
        : AbstractHandler<DeleteUserAsset, bool>(validator, logger)
    {
        protected override async Task<HandlerResponse<bool>> HandleRequest(
            DeleteUserAsset request,
            CancellationToken cancellationToken)
        {
            var existing = await userAssetRepository.GetByUserAndAssetAsync(
                request.UserId,
                request.AssetId,
                request.CurrencyId,
                cancellationToken);

            if (existing is null)
            {
                return HandlerResponse<bool>.NotFound("User asset not found.");
            }

            var deleted = await userAssetRepository.Delete(
                request.UserId,
                request.AssetId,
                request.CurrencyId,
                cancellationToken);
            return deleted
                ? HandlerResponse<bool>.Ok(deleted)
                : HandlerResponse<bool>.NotFound("User asset not found.");
        }
    }
}
