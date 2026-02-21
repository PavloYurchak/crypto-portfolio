
namespace CryptoPorfolio.Application.Models
{
    internal readonly struct TransactionTypes
    {
        public static readonly TransactionTypes Buy = new("BUY");
        public static readonly TransactionTypes Sell = new("SELL");

        private TransactionTypes(string value) => Value = value;

        public static TransactionTypes[] All => [Buy, Sell];

        public string Value { get; }

        public static implicit operator string(TransactionTypes value) => value.Value;
    }
}
