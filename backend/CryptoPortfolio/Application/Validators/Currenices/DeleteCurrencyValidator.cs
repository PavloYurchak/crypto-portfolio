// <copyright file="DeleteCurrencyValidator.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Requests.Currenices;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Currenices
{
    public class DeleteCurrencyValidator : AbstractValidator<DeleteCurrency>
    {
        public DeleteCurrencyValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
        }
    }
}
