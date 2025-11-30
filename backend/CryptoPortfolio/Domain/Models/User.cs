// <copyright file="User.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Domain.Models
{
    public sealed record User : AbstractModel
    {
        public int Id { get; set; }

        public required string Email { get; set; }

        public required string UserName { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool IsLockedOut { get; set; }

        public DateTime? LockoutEndAt { get; set; }

        public int FailedAccessCount { get; set; }

        public bool TwoFactorEnabled { get; set; }
    }
}
