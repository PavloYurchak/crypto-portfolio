// <copyright file="Asset.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Domain.Models
{
    public sealed record Asset : AbstractModel
    {
        public int Id { get; set; }

        public required string Symbol { get; set; }

        public required string Name { get; set; }
    }
}
