// <copyright file="UpdateAssetHandler.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Assets;
using CryptoPorfolio.Domain.Models;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Assets
{
    internal class UpdateAssetHandler(IAssetRepository assetRepository,
        IValidator<UpdateAsset> validator,
        ILogger<UpdateAssetHandler> logger)
        : AbstractHandler<UpdateAsset, Asset>(validator, logger)
    {
        protected override async Task<HandlerResponse<Asset>> HandleRequest(UpdateAsset request, CancellationToken cancellationToken)
        {
            var asset = await assetRepository.GetByIdAsync(request.Id, cancellationToken);
            return asset != null
                ? HandlerResponse<Asset>.Ok(asset!)
                : HandlerResponse<Asset>.NotFound("Asset not found!");
        }
    }
}
