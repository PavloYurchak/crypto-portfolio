
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Domain.Repositories
{
    public interface ITransactionTypeRepository : IDomainRepository
    {
        Task<IReadOnlyCollection<TransactionTypeModel>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TransactionTypeModel?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
