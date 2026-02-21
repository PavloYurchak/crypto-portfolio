
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    public interface IUserAssetRepository : IDomainRepository
    {
        Task<IReadOnlyCollection<UserAsset>> GetByUserAsync(int userId, CancellationToken cancellationToken = default);

        Task<UserAsset?> GetByUserAndAssetAsync(int userId, int assetId, CancellationToken cancellationToken = default);

        Task<UserAsset?> Create(UserAsset model, CancellationToken cancellationToken = default);

        Task<UserAsset?> Update(UserAsset model, CancellationToken cancellationToken = default);
    }
}
