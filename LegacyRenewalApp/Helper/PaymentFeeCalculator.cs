using LegacyRenewalApp.Interfaces;
using System;
using System.Collections.Generic;

namespace LegacyRenewalApp.Helper
{
    public class PaymentFeeCalculator : IPaymentFeeCalculator
    {
        private static readonly Dictionary<string, (decimal rate, string note)> PaymentFees =
            new Dictionary<string, (decimal, string)>
            {
                { "CARD", (0.020m, "card payment fee; ") },
                { "BANK_TRANSFER", (0.010m, "bank transfer fee; ") },
                { "PAYPAL", (0.035m, "paypal fee; ") },
                { "INVOICE", (0.000m, "invoice payment; ") },
            };

        public decimal CalculatePaymentFee(decimal amount, string paymentMethod)
        {
            if (!PaymentFees.TryGetValue(paymentMethod, out var entry))
                throw new ArgumentException("Unsupported payment method");

            return amount * entry.rate;
        }

        public string GetNote(string paymentMethod)
        {
            return PaymentFees.TryGetValue(paymentMethod, out var entry) ? entry.note : string.Empty;
        }
    }
}