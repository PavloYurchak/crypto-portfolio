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
    internal sealed class CreateUserAssetHandler(
        IUserAssetRepository userAssetRepository,
        IAssetRepository assetRepository,
        IDbTransactionService dbTransactionService,
        IValidator<AddUserAsset> validator,
        ILogger<CreateUserAssetHandler> logger)
        : AbstractHandler<AddUserAsset, UserAsset>(validator, logger)
    {
        protected override async Task<HandlerResponse<UserAsset>> HandleRequest(
            AddUserAsset request,
            CancellationToken cancellationToken)
        {
            var asset = await assetRepository.GetByIdAsync(request.AssetId, cancellationToken);
            if (asset is null)
            {
                return HandlerResponse<UserAsset>.NotFound("Asset not found.");
            }

            var existing = await userAssetRepository.GetByUserAndAssetAsync(
                request.UserId,
                request.AssetId,
                cancellationToken);

            if (existing is not null)
            {
                return new HandlerResponse<UserAsset>
                {
                    Success = false,
                    Error = "User asset already exists.",
                    StatusCode = 409,
                };
            }

            await dbTransactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                var added = await userAssetRepository.Create(request.ToModel(asset), cancellationToken);
                await dbTransactionService.CommitAsync(cancellationToken);

                return added is not null
                    ? HandlerResponse<UserAsset>.Ok(added)
                    : HandlerResponse<UserAsset>.Fail("Failed to create user asset.");
            }
            catch (Exception)
            {
                await dbTransactionService.RollbackAsync(cancellationToken);
                logger.LogError("Failed to create user asset for UserId {UserId} AssetId {AssetId}", request.UserId, request.AssetId);
                return HandlerResponse<UserAsset>.Fail("Failed to create user asset.");
            }
        }
    }
}
