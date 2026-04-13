using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyRenewalApp.Interfaces;
using LegacyRenewalApp.Models;

namespace LegacyRenewalApp.Helper
{
    public class DiscountCalculator
    {
        private readonly IDiscountStrategy[] _strategies;

        public DiscountCalculator()
        {
        }

        public DiscountCalculator(params IDiscountStrategy[] strategies)
        {
            _strategies = strategies;
        }

        public decimal CalculateDiscount(
            decimal baseAmount, Models.Customer customer, Models.SubscriptionPlan subscription,
            int seatCount, bool useLoyaltyPoints)
        {
            decimal totalDiscount = 0m;
            StringBuilder notesBuilder = new StringBuilder();
            foreach (var strategy in _strategies)
            {
                var discount = strategy.CalculateDiscount(baseAmount, customer, subscription, seatCount, useLoyaltyPoints);
                totalDiscount += discount.DiscountAmount;
                if (!string.IsNullOrEmpty(discount.Description))
                {
                    notesBuilder.Append(discount.Description).Append("; ");
                }
            }
            return totalDiscount;
        }
    }
}
