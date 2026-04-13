using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyRenewalApp.Interfaces;

namespace LegacyRenewalApp.Helper
{
    public class PaymentFeeCalculator : IPaymentFeeCalculator
    {
        private static readonly Dictionary<string, decimal> PaymentMethodFees = new Dictionary<string, decimal>
        {
            { "CreditCard", 0.02m },
            { "BankTransfer", 0.01m },
            { "PayPal", 0.035m },
            {  "Invoice", 0.00m   }
        };

        public decimal CalculatePaymentFee(decimal amount, string paymentMethod)
        {
            if (PaymentMethodFees.TryGetValue(paymentMethod, out var feeRate))
            {
                return amount * feeRate;
            }
            throw new ArgumentException($"Unsupported payment method: {paymentMethod}");
        }

        public string GetMethod(string paymentMethod)
        {
            return PaymentMethodFees.ContainsKey(paymentMethod) ? paymentMethod : "Unknown";
        }
    }
}
