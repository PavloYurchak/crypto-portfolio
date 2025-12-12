// <copyright file="GetAssets.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Abstractions.Messaging;

namespace CryptoPorfolio.Application.Requests.Assets
{
    public sealed record GetAssets() : IHandlerRequest<IEnumerable<Domain.Models.Asset>>;
}
