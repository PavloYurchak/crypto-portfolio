using CryptoPorfolio.Application.Response;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Mapping
{
    internal static class UserResponseMappingProfile
    {
        public static UserResponse ToUserResponse(this User entity)
        {
            return new UserResponse
            {
                Id = entity.Id,
                Email = entity.Email,
                UserName = entity.UserName,
                EmailConfirmed = entity.EmailConfirmed,
                IsLockedOut = entity.IsLockedOut,
                LockoutEndAt = entity.LockoutEndAt,
                FailedAccessCount = entity.FailedAccessCount,
                TwoFactorEnabled = entity.TwoFactorEnabled,
                UserType = entity.UserType,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }
    }
}
