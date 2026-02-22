using CryptoPorfolio.Application.Requests.Users;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Users
{
    public class GetCurrentUserValidator : AbstractValidator<GetCurrentUser>
    {
        public GetCurrentUserValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
