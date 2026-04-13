using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyRenewalApp.Models;

namespace LegacyRenewalApp.Helper
{
    public class SegmentDiscount
    {
        private static readonly Dictionary<string, decimal> SegmentRates = new Dictionary<string, decimal>
        {
            { "Silver", 0.05m },
            { "Gold", 0.10m },
            { "Platinum", 0.15m }
        };

        public DiscountResult Calculate(decimal baseAmount, Customer customer,
            SubscriptionPlan subscription, int seatCount, bool loyaltyPoints)
        {
            if (SegmentRates.TryGetValue(customer.Segment, out var entry))
                return new DiscountResult
                (baseAmount * entry, $"{customer.Segment} segment discount applied at {entry:P0} rate.");

            if (customer.Segment == "Education" && subscription.IsEducationEligible)
                return new DiscountResult
                (baseAmount * 0.20m, "Education segment discount applied at 20% rate.");

            return DiscountResult.Empty;
        }
    }
}
