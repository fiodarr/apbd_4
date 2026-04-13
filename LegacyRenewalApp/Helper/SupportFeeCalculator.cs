using System.Collections.Generic;
using LegacyRenewalApp.Interfaces;

namespace LegacyRenewalApp.Helper
{
    public class SupportFeeCalculator : ISupportFeeCalculator
    {
        public static readonly Dictionary<string, decimal> PlanSupportFees = new Dictionary<string, decimal>
        {
            { "START", 250m },
            { "PRO", 400m },
            { "ENTERPRISE", 700m }
        };

        public decimal CalculateSupportFee(string planCode)
        {
            return PlanSupportFees.TryGetValue(planCode, out var fee) ? fee : 0m;
        }
    }
}