// <copyright file="Unit.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Application.Abstractions.Messaging
{
    public readonly record struct Unit
    {
        public static readonly Unit Value = default(Unit);
    }
}
