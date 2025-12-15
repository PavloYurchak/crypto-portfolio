// <copyright file="PasswordHasher.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using System.Security.Cryptography;
using CryptoPorfolio.Application.Abstractions.Security;

namespace CryptoPorfolio.Infrastructure.Services.Security
{
    internal sealed class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100_000;
        private const string AlgorithmName = "PBKDF2-SHA256";

        public string HashPassword(string password, out string? salt, out string? algorithm)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256);

            var key = pbkdf2.GetBytes(KeySize);

            salt = Convert.ToBase64String(saltBytes);
            algorithm = AlgorithmName;

            return Convert.ToBase64String(key);
        }

        public bool VerifyPassword(string password, string hash, string? salt, string? algorithm)
        {
            if (string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(salt))
            {
                return false;
            }

            var saltBytes = Convert.FromBase64String(salt);
            var hashBytes = Convert.FromBase64String(hash);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                Iterations,
                HashAlgorithmName.SHA256);

            var computed = pbkdf2.GetBytes(KeySize);

            return CryptographicOperations.FixedTimeEquals(hashBytes, computed);
        }
    }
}
