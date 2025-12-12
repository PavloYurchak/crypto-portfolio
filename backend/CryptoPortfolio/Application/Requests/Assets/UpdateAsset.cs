// <copyright file="UpdateAsset.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.Assets
{
    public sealed record UpdateAsset(int Id) : IHandlerRequest<Domain.Models.Asset>
    {
        public required string Symbol { get; init; }

        public required string Name { get; init; }
    }
}
