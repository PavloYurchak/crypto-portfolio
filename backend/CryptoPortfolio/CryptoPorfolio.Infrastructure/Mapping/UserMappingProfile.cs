// <copyright file="UserMappingProfile.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>

using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Infrastructure.Mapping
{
    internal static class UserMappingProfile
    {
        public static User ToModel(this Entities.User entity)
        {
            return new User
            {
                Id = entity.Id,
                Email = entity.Email,
                UserName = entity.UserName,
                EmailConfirmed = entity.EmailConfirmed,
                IsLockedOut = entity.IsLockedOut,
                LockoutEndAt = entity.LockoutEndAt,
                FailedAccessCount = entity.FailedAccessCount,
                TwoFactorEnabled = entity.TwoFactorEnabled,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
            };
        }

        public static Entities.User ToEntity(this User model)
        {
            return new Entities.User
            {
                Email = model.Email,
                UserName = model.UserName,
                EmailConfirmed = model.EmailConfirmed,
                IsLockedOut = model.IsLockedOut,
                LockoutEndAt = model.LockoutEndAt,
                FailedAccessCount = model.FailedAccessCount,
                TwoFactorEnabled = model.TwoFactorEnabled,
                IsActive = model.IsActive,
            };
        }
    }
}
