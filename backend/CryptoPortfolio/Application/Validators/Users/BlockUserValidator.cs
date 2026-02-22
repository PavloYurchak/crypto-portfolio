using CryptoPorfolio.Application.Requests.Users;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Users
{
    public class BlockUserValidator : AbstractValidator<BlockUser>
    {
        public BlockUserValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
