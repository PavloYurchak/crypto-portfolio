
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Infrastructure.Mapping
{
    internal static class CurrencyMappingProfile
    {
        public static Currency ToModel(this Entities.Currency entity)
        {
            return new Currency
            {
                Id = entity.Id,
                Symbol = entity.Symbol,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
            };
        }

        public static Entities.Currency ToEntity(this Currency model)
        {
            return new Entities.Currency
            {
                Symbol = model.Symbol,
                Name = model.Name,
                IsActive = model.IsActive,
            };
        }
    }
}
