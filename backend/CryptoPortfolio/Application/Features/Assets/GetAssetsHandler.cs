// <copyright file="GetAssetsHandler.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.Assets;
using CryptoPorfolio.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CryptoPorfolio.Application.Features.Assets
{
    internal class GetAssetsHandler(IAssetRepository assetRepository,
        IValidator<GetAssets> validator,
        ILogger<GetAssetsHandler> logger)
        : AbstractHandler<GetAssets, IEnumerable<Domain.Models.Asset>>(validator, logger)
    {
        protected override async Task<HandlerResponse<IEnumerable<Domain.Models.Asset>>> HandleRequest(GetAssets request, CancellationToken ct)
        {
            var assets = await assetRepository.GetAllAsync();
            return HandlerResponse<IEnumerable<Domain.Models.Asset>>.Ok(assets);
        }
    }
}
