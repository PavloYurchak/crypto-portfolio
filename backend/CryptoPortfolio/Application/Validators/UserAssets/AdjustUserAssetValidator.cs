using CryptoPorfolio.Application.Requests.UserAssets;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.UserAssets
{
    public class AdjustUserAssetValidator : AbstractValidator<AdjustUserAsset>
    {
        public AdjustUserAssetValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
            RuleFor(x => x.AssetId)
                .GreaterThan(0);
            RuleFor(x => x.CurrencyId)
                .GreaterThan(0);
            RuleFor(x => x.DeltaQuantity)
                .NotEqual(0);
            RuleFor(x => x.Price)
                .GreaterThan(0);
        }
    }
}
