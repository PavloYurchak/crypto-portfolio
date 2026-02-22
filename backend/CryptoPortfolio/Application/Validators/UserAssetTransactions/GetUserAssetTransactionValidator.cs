using CryptoPorfolio.Application.Requests.UserAssetTransactions;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.UserAssetTransactions
{
    public class GetUserAssetTransactionValidator : AbstractValidator<GetUserAssetTransaction>
    {
        public GetUserAssetTransactionValidator()
        {
            RuleFor(x => x.TransactionId)
                .GreaterThan(0);
            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}
