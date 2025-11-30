// <copyright file="UserAsset.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Domain.Models
{
    public sealed record UserAsset : AbstractModel
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public int AssetId { get; set; }

        public required string AssetSymbol { get; set; }

        public required string AssetName { get; set; }

        public decimal Quantity { get; set; }
    }
}
