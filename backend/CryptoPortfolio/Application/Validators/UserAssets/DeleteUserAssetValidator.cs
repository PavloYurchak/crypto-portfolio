using CryptoPorfolio.Application.Requests.UserAssets;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.UserAssets
{
    public class DeleteUserAssetValidator : AbstractValidator<DeleteUserAsset>
    {
        public DeleteUserAssetValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
            RuleFor(x => x.AssetId)
                .GreaterThan(0);
        }
    }
}
