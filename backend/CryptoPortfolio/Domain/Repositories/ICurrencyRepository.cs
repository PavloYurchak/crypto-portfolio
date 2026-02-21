
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    public interface ICurrencyRepository : IDomainRepository
    {
        Task<IReadOnlyCollection<Currency>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Currency?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<Currency?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken = default);

        Task<Currency> CreateAsync(Currency currency, CancellationToken cancellationToken = default);

        Task<Currency?> UpdateAsync(Currency currency, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
