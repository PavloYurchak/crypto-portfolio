using CryptoPorfolio.Application.Requests.UserAssetTransactions;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.UserAssetTransactions
{
    public class AddUserAssetTransactionValidator : AbstractValidator<AddUserAssetTransaction>
    {
        public AddUserAssetTransactionValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
            RuleFor(x => x.AssetId)
                .GreaterThan(0);
            RuleFor(x => x.CurrencyId)
                .GreaterThan(0);
            RuleFor(x => x.TransactionTypeCode)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Quantity)
                .GreaterThan(0);
            RuleFor(x => x.Amount)
                .GreaterThan(0);
            RuleFor(x => x.Price)
                .GreaterThan(0);
            RuleFor(x => x.ExecutedAt)
                .NotEmpty();
        }
    }
}
