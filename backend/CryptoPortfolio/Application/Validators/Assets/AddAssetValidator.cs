// <copyright file="AddAssetValidator.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Requests.Assets;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Assets
{
    internal class AddAssetValidator : AbstractValidator<AddAsset>
    {
        public AddAssetValidator()
        {
            this.RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(20);
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
