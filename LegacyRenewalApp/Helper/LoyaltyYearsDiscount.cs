using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyRenewalApp.Interfaces;
using LegacyRenewalApp.Models;

namespace LegacyRenewalApp.Helper
{
    public class LoyaltyYearsDiscount : IDiscountStrategy
    {
        public DiscountResult CalculateDiscount(decimal baseAmount, Models.Customer customer,
            Models.SubscriptionPlan subscription, int seatCount, bool loyaltyPoints)
        {
            if (customer.YearsWithCompany > 0)
            {
                decimal discountRate = 0.02m * customer.YearsWithCompany;
                decimal discountAmount = baseAmount * discountRate;
                string description = $"Loyalty Discount: {customer.YearsWithCompany} years = {discountAmount:C2}";
                return new DiscountResult(discountAmount, description);
            }
            return DiscountResult.Empty;
        }

        DiscountResult IDiscountStrategy.CalculateDiscount(decimal baseAmount, Customer customer, SubscriptionPlan subscription, int seatCount, bool loyaltyPoints)
        {
            throw new NotImplementedException();
        }
    }
}
