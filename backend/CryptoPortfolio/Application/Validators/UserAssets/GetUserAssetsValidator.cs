using CryptoPorfolio.Application.Requests.UserAssets;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.UserAssets
{
    public class GetUserAssetsValidator : AbstractValidator<GetUserAssets>
    {
        public GetUserAssetsValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}
