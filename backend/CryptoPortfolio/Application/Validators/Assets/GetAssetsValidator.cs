// <copyright file="GetAssetsValidator.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Requests.Assets;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.Assets
{
    public class GetAssetsValidator : AbstractValidator<GetAssets>;
}
