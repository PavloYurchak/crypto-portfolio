
using CryptoPorfolio.Application.Requests.Currenices;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Currenices
{
    public class AddCurrencyValidation : AbstractValidator<AddCurrency>
    {
        public AddCurrencyValidation()
        {
            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
