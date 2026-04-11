using LegacyRenewalApp.Interfaces;
using LegacyRenewalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Helper 
{
    public class BillingGatewayAdapter : IBillingGateway
    {

        public void saveInvoice(RenewalInvoice invoice)
        {
            LegacyBillingGateway.SaveInvoice(invoice);
        }

        public void sendEmail(string email, string subject, string body)
        {
            LegacyBillingGateway.SendEmail(email, subject, body);
        }
    }
}
