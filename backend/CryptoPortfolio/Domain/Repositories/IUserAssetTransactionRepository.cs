
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    public interface IUserAssetTransactionRepository : IDomainRepository
    {
        Task<IEnumerable<UserAssetTransaction>> GetUserTransactionsAsync(
            long userAssetId,
            CancellationToken cancellationToken = default);

        Task<UserAssetTransaction?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<UserAssetTransaction> CreateAsync(UserAssetTransaction model, CancellationToken cancellationToken = default);
    }
}
