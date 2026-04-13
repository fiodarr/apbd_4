using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyRenewalApp.Interfaces;
using LegacyRenewalApp.Models;

namespace LegacyRenewalApp.Helper
{
    public class SeatCountDiscount :IDiscountStrategy
    {
        public DiscountResult CalculateDiscount(decimal baseAmount, Customer customer, SubscriptionPlan subscription, int seatCount, bool loyaltyPoints)
        {
            if (seatCount >= 50)
            {
                return new DiscountResult(baseAmount * 0.12m, $"Seat Count Discount: {seatCount} seats = {(baseAmount * 0.12m):C2}");
            }

            if (seatCount >= 20)
            {
                return new DiscountResult(baseAmount * 0.08m, $"Seat Count Discount: {seatCount} seats = {(baseAmount * 0.08m):C2}");
            }

            if (seatCount >= 10)
            {
                return new DiscountResult(baseAmount * 0.04m, $"Seat Count Discount: {seatCount} seats = {(baseAmount * 0.4m):C2}");
            }
            return DiscountResult.Empty;
        }
    }
}
