namespace CryptoPorfolio.Application.Response
{
    public sealed record UserResponse
    {
        public int Id { get; init; }

        public required string Email { get; init; }

        public required string UserName { get; init; }

        public bool EmailConfirmed { get; init; }

        public bool IsLockedOut { get; init; }

        public DateTime? LockoutEndAt { get; init; }

        public int FailedAccessCount { get; init; }

        public bool TwoFactorEnabled { get; init; }

        public required string UserType { get; init; }

        public bool IsActive { get; init; }

        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
