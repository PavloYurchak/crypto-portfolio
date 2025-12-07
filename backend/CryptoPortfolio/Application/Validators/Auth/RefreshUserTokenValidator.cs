// <copyright file="RefreshUserTokenValidator.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Requests.Auth;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Auth
{
    public sealed class RefreshUserTokenValidator : AbstractValidator<RefreshUserToken>
    {
        public RefreshUserTokenValidator()
        {
            this.RuleFor(x => x.RefreshToken)
                .NotEmpty();
        }
    }
}
