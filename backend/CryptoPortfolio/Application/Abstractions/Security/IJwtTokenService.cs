
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Abstractions.Security
{
    public interface IJwtTokenService
    {
        (string Token, DateTime ExpiresAt) GenerateAccessToken(User user);

        string GenerateRefreshToken();
    }
}
