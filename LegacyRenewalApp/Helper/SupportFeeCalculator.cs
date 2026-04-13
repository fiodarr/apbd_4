using LegacyRenewalApp.Interfaces;

namespace LegacyRenewalApp.Helper
{
    public class SupportFeeCalculator : ISupportFeeCalculator
    {
        public decimal CalculateSupportFee(string planCode)
        {
            if (planCode.ToUpperInvariant().Contains("PREMIUM"))
            {
                return 50m;
            }
            return 0m;
        }
    }
}