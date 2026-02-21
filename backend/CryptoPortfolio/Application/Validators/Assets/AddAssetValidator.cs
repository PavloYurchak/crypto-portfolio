
using CryptoPorfolio.Application.Requests.Assets;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Assets
{
    public class AddAssetValidator : AbstractValidator<AddAsset>
    {
        public AddAssetValidator()
        {
            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(20);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
