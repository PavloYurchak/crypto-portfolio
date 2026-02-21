#nullable enable
using System;
using System.Collections.Generic;

namespace CryptoPorfolio.Infrastructure.Entities;

public partial class UserRefreshToken
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? ReasonRevoked { get; set; }

    public bool IsActive { get; set; }

    public virtual User User { get; set; } = null!;
}