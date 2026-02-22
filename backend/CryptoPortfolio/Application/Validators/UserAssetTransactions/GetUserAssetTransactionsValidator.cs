using CryptoPorfolio.Application.Requests.UserAssetTransactions;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.UserAssetTransactions
{
    public class GetUserAssetTransactionsValidator : AbstractValidator<GetUserAssetTransactions>
    {
        public GetUserAssetTransactionsValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
            RuleFor(x => x.AssetId)
                .GreaterThan(0);
        }
    }
}
