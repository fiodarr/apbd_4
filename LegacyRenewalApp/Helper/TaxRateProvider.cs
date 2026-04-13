using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyRenewalApp.Interfaces;

namespace LegacyRenewalApp.Helper
{
    public class TaxRateProvider : ITaxRateProvider
    {
        private static readonly Dictionary<string, decimal> TaxRates = new Dictionary<string, decimal>
        {
            { "Poland", 0.23m },
            { "Germany", 0.19m },
            { "Czech Republic", 0.21m },
            { "Norway", 0.25m }
        };

        private const decimal DefaultTaxRate = 0.20m;

        private decimal GetTaxRate(string country)
        {
            return TaxRates.TryGetValue(country, out var rate) ? rate : DefaultTaxRate;
        }

        public decimal getRate(string country)
        {
            throw new NotImplementedException();
        }

        decimal ITaxRateProvider.GetTaxRate(string country)
        {
            return GetTaxRate(country);
        }
    }
}
