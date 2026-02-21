
using CryptoPorfolio.Application.Requests.Auth;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Auth
{
    public sealed class LoginUserValidator : AbstractValidator<LoginUser>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
