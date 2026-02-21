
namespace CryptoPorfolio.Application.Models
{
    internal readonly struct UserTypes
    {
        public static readonly UserTypes Admin = new("Admin");
        public static readonly UserTypes User = new("User");

        private UserTypes(string value) => Value = value;

        public static UserTypes[] All => [Admin, User];

        public string Value { get; }

        public static implicit operator string(UserTypes value) => value.Value;
    }
}
