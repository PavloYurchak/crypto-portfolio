
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    public interface IUserRepository : IDomainRepository
    {
        Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<User> CreateAsync(User model, CancellationToken cancellationToken = default);

        Task<User> CreateWithPasswordAsync(User model, string passwordHash, string passwordSalt, string passwordAlgo, CancellationToken cancellationToken = default);

        Task<User> UpdateAsync(User model, CancellationToken cancellationToken = default);

        Task<bool> SoftDeleteAsync(int id, CancellationToken cancellationToken = default);

        Task<User?> GetInternalByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<User?> UpdatePasswordAsync(
            int userId,
            string passwordHash,
            string? passwordSalt,
            string? passwordAlgo,
            CancellationToken cancellationToken = default);

        Task<bool> IsUserEmptyAsync(CancellationToken cancellationToken = default);
    }
}
