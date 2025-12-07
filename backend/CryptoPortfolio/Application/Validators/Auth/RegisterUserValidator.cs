// <copyright file="RegisterUserValidator.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Requests.Auth;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Auth
{
    public sealed class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        public RegisterUserValidator()
        {
            this.RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            this.RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            this.RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
