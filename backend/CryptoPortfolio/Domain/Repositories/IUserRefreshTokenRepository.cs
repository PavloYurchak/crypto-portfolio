
namespace CryptoPorfolio.Domain.Repositories
{
    public interface IUserRefreshTokenRepository : IDomainRepository
    {
        Task CreateAsync(int userId, string token, DateTime expiresAt, CancellationToken cancellationToken = default);

        Task<(int UserId, bool IsActive, DateTime ExpiresAt)?> GetAsync(string token, CancellationToken cancellationToken = default);

        Task RevokeAsync(string token, string? reason = null, CancellationToken cancellationToken = default);
    }
}
