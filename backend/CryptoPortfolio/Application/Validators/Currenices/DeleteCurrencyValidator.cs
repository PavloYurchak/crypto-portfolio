
using CryptoPorfolio.Application.Requests.Currenices;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Currenices
{
    public class DeleteCurrencyValidator : AbstractValidator<DeleteCurrency>
    {
        public DeleteCurrencyValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
