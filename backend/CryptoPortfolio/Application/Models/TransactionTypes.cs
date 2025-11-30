// <copyright file="TransactionTypes.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

namespace CryptoPorfolio.Application.Models
{
    internal readonly struct TransactionTypes
    {
        public static readonly TransactionTypes Buy = new("BUY");
        public static readonly TransactionTypes Sell = new("SELL");

        private TransactionTypes(string value) => this.Value = value;

        public static TransactionTypes[] All => [Buy, Sell];

        public string Value { get; }

        public static implicit operator string(TransactionTypes value) => value.Value;
    }
}
