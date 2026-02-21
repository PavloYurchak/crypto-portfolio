
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Infrastructure.Mapping
{
    internal static class TransactionTypeMappingProfile
    {
        public static TransactionTypeModel ToModel(this Entities.TransactionType entity)
        {
            return new TransactionTypeModel
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
            };
        }

        public static Entities.TransactionType ToEntity(this TransactionTypeModel model)
        {
            return new Entities.TransactionType
            {
                Code = model.Code,
                Name = model.Name,
                Description = model.Description,
                IsActive = model.IsActive,
            };
        }
    }
}
