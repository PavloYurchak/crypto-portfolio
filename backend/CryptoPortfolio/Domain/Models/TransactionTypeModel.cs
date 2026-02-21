
namespace CryptoPorfolio.Domain.Models
{
    public record TransactionTypeModel : AbstractModel
    {
        public int Id { get; set; }

        public required string Code { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
