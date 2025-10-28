// <copyright file="TestMappingProfile.cs" company="CryptoPorfolio">
// Copyright (c) CryptoPorfolio. All rights reserved.
// </copyright>
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Infrastructure.Mapping
{
    internal static class TestMappingProfile
    {
        public static Test ToModel(this Entities.Test entity)
        {
            return new Test
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                DeletedAt = entity.DeletedAt,
                IsActive = entity.IsActive,
            };
        }

        public static Entities.Test ToEntity(this Test model)
        {
            return new Entities.Test
            {
                Name = model.Name,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                DeletedAt = model.DeletedAt,
                IsActive = model.IsActive,
            };
        }
    }
}
