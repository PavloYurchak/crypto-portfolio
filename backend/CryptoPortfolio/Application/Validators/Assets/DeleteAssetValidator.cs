
using CryptoPorfolio.Application.Requests.Assets;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Assets
{
    public class DeleteAssetValidator : AbstractValidator<DeleteAsset>
    {
        public DeleteAssetValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
