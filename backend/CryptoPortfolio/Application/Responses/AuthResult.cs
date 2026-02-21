
namespace CryptoPorfolio.Application.Response
{
    public record AuthResult
    {
        public int UserId { get; init; }

        public required string Email { get; init; }

        public required string UserName { get; init; }

        public required string AccessToken { get; init; }

        public DateTime AccessTokenExpiresAt { get; init; }

        public required string RefreshToken { get; init; }
    }
}
