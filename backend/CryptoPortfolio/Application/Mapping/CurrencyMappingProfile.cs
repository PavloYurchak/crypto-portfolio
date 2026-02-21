
using CryptoPorfolio.Application.Requests.Assets;
using CryptoPorfolio.Application.Requests.Currenices;
using CryptoPorfolio.Domain.Models;

namespace CryptoPorfolio.Application.Mapping
{
    internal static class CurrencyMappingProfile
    {
        public static Currency ToModel(this AddCurrency asset)
        {
            return new Currency
            {
                Symbol = asset.Symbol,
                Name = asset.Name,
            };
        }

        public static Currency ToModel(this UpdateCurrency asset)
        {
            return new Currency
            {
                Id = asset.Id,
                Symbol = asset.Symbol,
                Name = asset.Name,
            };
        }
    }
}
