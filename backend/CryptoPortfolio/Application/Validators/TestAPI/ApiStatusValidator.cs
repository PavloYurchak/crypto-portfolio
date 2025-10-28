// <copyright file="ApiStatusValidator.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Application.Requests.TestAPI;
using FluentValidation;

namespace CryptoPorfolio.Application.Validators.TestAPI
{
    public sealed class ApiStatusValidator : AbstractValidator<ApiStatus>;
}
