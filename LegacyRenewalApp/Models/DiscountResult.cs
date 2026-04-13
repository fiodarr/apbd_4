using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Models
{
    public class DiscountResult
    {
        public decimal DiscountAmount { get; set; }
        public string Description { get; set; }
        
        public static readonly DiscountResult Empty = new DiscountResult(0m, string.Empty);

        public DiscountResult(decimal discountAmount, string description)
        {
            DiscountAmount = discountAmount;
            Description = description ?? string.Empty;
        }
    }
}
