// <copyright file="DeleteAsset.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Requests.Assets;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Assets
{
    internal class DeleteAssetValidator : AbstractValidator<DeleteAsset>
    {
        public DeleteAssetValidator()
        {
            this.RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
