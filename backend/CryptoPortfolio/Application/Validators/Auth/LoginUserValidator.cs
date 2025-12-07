// <copyright file="LoginUserValidator.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Requests.Auth;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Auth
{
    public sealed class LoginUserValidator : AbstractValidator<LoginUser>
    {
        public LoginUserValidator()
        {
            this.RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            this.RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
