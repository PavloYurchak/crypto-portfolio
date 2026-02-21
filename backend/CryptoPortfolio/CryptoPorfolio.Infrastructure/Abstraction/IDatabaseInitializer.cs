
namespace CryptoPorfolio.Infrastructure.Abstraction
{
    public interface IDatabaseInitializer
    {
        Task InitializeAndSeed(CancellationToken cancellationToken = default(CancellationToken));
    }
}
