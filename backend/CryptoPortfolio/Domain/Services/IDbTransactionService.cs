
namespace CryptoPorfolio.Domain.Services
{
    public interface IDbTransactionService
    {
        Task BeginTransactionAsync(CancellationToken ct);

        Task CommitAsync(CancellationToken ct);

        Task RollbackAsync(CancellationToken ct);
    }
}
