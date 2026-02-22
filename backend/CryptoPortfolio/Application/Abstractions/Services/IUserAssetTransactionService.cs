using CryptoPorfolio.Application.Abstractions.Messaging;
using CryptoPorfolio.Application.Requests.UserAssetTransactions;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Abstractions.Services
{
    public interface IUserAssetTransactionService : IApplicationService
    {
        Task<HandlerResponse<UserAssetTransaction>> CreateAsync(
            AddUserAssetTransaction request,
            CancellationToken cancellationToken = default);
    }
}
