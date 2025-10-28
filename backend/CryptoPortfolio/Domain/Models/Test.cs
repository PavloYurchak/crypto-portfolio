// <copyright file="Test.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Domain.Models
{
    public sealed record Test
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
