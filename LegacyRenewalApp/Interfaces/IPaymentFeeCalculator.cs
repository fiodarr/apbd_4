using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Interfaces
{
    public interface IPaymentFeeCalculator
    {
        decimal CalculatePaymentFee(decimal amount, string paymentMethod);
        string GetMethod(string paymentMethod);
    }
}
