// <copyright file="AddCurrencyValidation.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Requests.Currenices;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Currenices
{
    internal class AddCurrencyValidation : AbstractValidator<AddCurrency>
    {
        public AddCurrencyValidation()
        {
            this.RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(10);

            this.RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
