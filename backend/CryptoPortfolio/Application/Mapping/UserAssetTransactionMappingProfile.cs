using CryptoPorfolio.Application.Requests.UserAssetTransactions;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Mapping
{
    internal static class UserAssetTransactionMappingProfile
    {
        public static UserAssetTransaction ToModel(
            this AddUserAssetTransaction request,
            UserAsset userAsset,
            Currency currency,
            TransactionTypeModel transactionType)
        {
            return new UserAssetTransaction
            {
                UserId = request.UserId,
                UserAssetId = userAsset.Id,
                AssetId = userAsset.AssetId,
                AssetSymbol = userAsset.AssetSymbol,
                CurrencyId = currency.Id,
                CurrencySymbol = currency.Symbol,
                TransactionTypeId = transactionType.Id,
                TransactionTypeCode = transactionType.Code,
                Quantity = request.Quantity,
                Amount = request.Amount,
                Price = request.Price,
                ExecutedAt = request.ExecutedAt,
            };
        }
    }
}
