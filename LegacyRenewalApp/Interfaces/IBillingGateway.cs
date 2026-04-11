using LegacyRenewalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyRenewalApp.Interfaces
{
    public interface IBillingGateway
    {
        void saveInvoice(RenewalInvoice invoice);
        void sendEmail(string email, string subject, string body);
    }
}
