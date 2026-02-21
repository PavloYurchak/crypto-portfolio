
using CryptoPorfolio.Application.Requests.Currenices;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Currenices
{
    public class UpdateCurrencyValidator : AbstractValidator<UpdateCurrency>
    {
        public UpdateCurrencyValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(10);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
