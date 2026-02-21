
using CryptoPorfolio.Application.Requests.Assets;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Assets
{
    public class UpdateAssetsValidator : AbstractValidator<UpdateAsset>
    {
        public UpdateAssetsValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(20);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
