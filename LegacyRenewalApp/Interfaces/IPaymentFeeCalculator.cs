using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Interfaces
{
    public interface IPaymentFeeCalculator
    {
        decimal Calculate(string normalizedPaymentMethod, decimal paymentFeeBase);
        decimal CalculateFee(decimal amount, string paymentMethod);
        string GetNote(string paymentMethod);
    }
}
