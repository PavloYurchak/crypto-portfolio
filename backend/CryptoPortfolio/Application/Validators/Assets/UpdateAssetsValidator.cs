// <copyright file="UpdateAssetsValidator.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Requests.Assets;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Assets
{
    public class UpdateAssetsValidator : AbstractValidator<UpdateAsset>
    {
        public UpdateAssetsValidator()
        {
            this.RuleFor(x => x.Id)
                .NotEmpty();
            this.RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(20);
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
