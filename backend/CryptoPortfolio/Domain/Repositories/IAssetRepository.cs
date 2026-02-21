
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    public interface IAssetRepository : IDomainRepository
    {
        Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Asset?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken = default);

        Task<Asset?> CreateAssetAsync(Asset model, CancellationToken cancellationToken = default);

        Task<Asset?> UpdateAssetAsync(Asset model, CancellationToken cancellationToken = default);

        Task<bool> DeleteAssetAsync(int id, CancellationToken cancellationToken = default);
    }
}
