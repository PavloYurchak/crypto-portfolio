using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Mapping;
using CryptoPorfolio.Application.Requests.UserAssets;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using CryptoPorfolio.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.UserAssets
{
    internal sealed class UpdateUserAssetHandler(
        IUserAssetRepository userAssetRepository,
        IDbTransactionService dbTransactionService,
        IValidator<UpdateUserAsset> validator,
        ILogger<UpdateUserAssetHandler> logger)
        : AbstractHandler<UpdateUserAsset, UserAsset>(validator, logger)
    {
        protected override async Task<HandlerResponse<UserAsset>> HandleRequest(
            UpdateUserAsset request,
            CancellationToken cancellationToken)
        {
            var existing = await userAssetRepository.GetByUserAndAssetAsync(
                request.UserId,
                request.AssetId,
                cancellationToken);

            if (existing is null)
            {
                return HandlerResponse<UserAsset>.NotFound("User asset not found.");
            }

            await dbTransactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                var updated = await userAssetRepository.Update(request.ToModel(existing), cancellationToken);
                await dbTransactionService.CommitAsync(cancellationToken);

                return updated is not null
                    ? HandlerResponse<UserAsset>.Ok(updated)
                    : HandlerResponse<UserAsset>.NotFound("User asset not found.");
            }
            catch (Exception)
            {
                await dbTransactionService.RollbackAsync(cancellationToken);
                logger.LogError("Failed to update user asset for UserId {UserId} AssetId {AssetId}", request.UserId, request.AssetId);
                return HandlerResponse<UserAsset>.Fail("Failed to update user asset.");
            }
        }
    }
}
