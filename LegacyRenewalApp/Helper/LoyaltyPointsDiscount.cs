using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyRenewalApp.Interfaces;
using LegacyRenewalApp.Models;

namespace LegacyRenewalApp.Helper
{
    public class LoyaltyPointsDiscount : IDiscountStrategy
    {
        private const int MaxLoyaltyPoints = 1000;

        public DiscountResult CalculateDiscount(decimal baseAmount, Customer customer, SubscriptionPlan subscription, int seatCount, bool loyaltyPoints)
        {
            if (loyaltyPoints && customer.LoyaltyPoints > 0)
            {
                int pointsToUse = Math.Min(customer.LoyaltyPoints, MaxLoyaltyPoints);
                decimal discountAmount = pointsToUse * 0.01m;
                string description = $"Loyalty Points Discount: {pointsToUse} points = {discountAmount:C2}";
                return new DiscountResult(discountAmount, description);
            }
            return DiscountResult.Empty;
        }

    }
}
