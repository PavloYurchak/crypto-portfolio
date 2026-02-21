#nullable enable
using System;
using System.Collections.Generic;

namespace CryptoPorfolio.Infrastructure.Entities;

public partial class UserAsset
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public int AssetId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Asset Asset { get; set; } = null!;

    public virtual ICollection<UserAssetTransaction> UserAssetTransactions { get; set; } = new List<UserAssetTransaction>();
}