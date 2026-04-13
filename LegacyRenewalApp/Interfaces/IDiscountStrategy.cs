using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyRenewalApp.Models;

namespace LegacyRenewalApp.Interfaces
{
    public interface IDiscountStrategy
    {
        DiscountResult CalculateDiscount(decimal baseAmount, Customer customer,
            SubscriptionPlan subscription, int seatCount, bool loyaltyPoints);
    }
}
