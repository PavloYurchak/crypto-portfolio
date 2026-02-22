using CryptoPorfolio.Application.Response;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.UnitTests.Helpers
{
    public static class TestModels
    {
        public static AuthResult AuthResult()
            => new()
            {
                UserId = 1,
                Email = "user@test.local",
                UserName = "user",
                AccessToken = "access",
                AccessTokenExpiresAt = DateTime.UtcNow.AddHours(1),
                RefreshToken = "refresh",
            };

        public static UserResponse UserResponse()
            => new()
            {
                Id = 1,
                Email = "user@test.local",
                UserName = "user",
                EmailConfirmed = true,
                IsLockedOut = false,
                LockoutEndAt = null,
                FailedAccessCount = 0,
                TwoFactorEnabled = false,
                UserType = "User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = null,
            };

        public static Asset Asset()
            => new()
            {
                Id = 1,
                Symbol = "BTC",
                Name = "Bitcoin",
            };

        public static Currency Currency()
            => new()
            {
                Id = 1,
                Symbol = "USD",
                Name = "US Dollar",
            };

        public static UserAsset UserAsset()
            => new()
            {
                Id = 1,
                UserId = 1,
                AssetId = 1,
                AssetSymbol = "BTC",
                AssetName = "Bitcoin",
                Quantity = 1.5m,
            };

        public static UserAssetTransaction UserAssetTransaction()
            => new()
            {
                Id = 1,
                UserId = 1,
                UserAssetId = 1,
                AssetId = 1,
                AssetSymbol = "BTC",
                CurrencyId = 1,
                CurrencySymbol = "USD",
                TransactionTypeId = 1,
                TransactionTypeCode = "BUY",
                Quantity = 1.5m,
                Amount = 30000m,
                Price = 20000m,
                ExecutedAt = DateTime.UtcNow.AddMinutes(-5),
            };
    }
}
