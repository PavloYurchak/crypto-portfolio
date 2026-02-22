using CryptoPorfolio.Application.Requests.UserAssets;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.UserAssets
{
    public class UpdateUserAssetValidator : AbstractValidator<UpdateUserAsset>
    {
        public UpdateUserAssetValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
            RuleFor(x => x.AssetId)
                .GreaterThan(0);
            RuleFor(x => x.CurrencyId)
                .GreaterThan(0);
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Price)
                .GreaterThan(0);
        }
    }
}
