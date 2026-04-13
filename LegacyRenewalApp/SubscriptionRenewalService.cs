using LegacyRenewalApp.Helper;
using LegacyRenewalApp.Interfaces;
using LegacyRenewalApp.Models;
using LegacyRenewalApp.Repositories;
using System;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISubscriptionPlanRepository _planRepository;
        private readonly IRenewalServiceValidator _validator;
        private readonly IBillingGateway _billingGateway;
        private readonly DiscountCalculator _discountCalculator;
        private readonly ITaxRateProvider _taxRateProvider;
        private readonly IPaymentFeeCalculator _paymentFeeCalculator;
        private readonly ISupportFeeCalculator _supportFeeCalculator;

        public SubscriptionRenewalService()
            : this(
                new CustomerRepository(),
                new SubscriptionPlanRepository(),
                new RenewalServiceValidator(),
                new BillingGatewayAdapter(),
                new DiscountCalculator(),
                new TaxRateProvider(),
                new PaymentFeeCalculator(),
                new SupportFeeCalculator())
        {
        }

        public SubscriptionRenewalService(
            ICustomerRepository customerRepository,
            ISubscriptionPlanRepository planRepository,
            IRenewalServiceValidator validator,
            IBillingGateway billingGateway,
            DiscountCalculator discountCalculator,
            ITaxRateProvider taxRateProvider,
            IPaymentFeeCalculator paymentFeeCalculator,
            ISupportFeeCalculator supportFeeCalculator)
        {
            _customerRepository = customerRepository;
            _planRepository = planRepository;
            _validator = validator;
            _billingGateway = billingGateway;
            _discountCalculator = discountCalculator;
            _taxRateProvider = taxRateProvider;
            _paymentFeeCalculator = paymentFeeCalculator;
            _supportFeeCalculator = supportFeeCalculator;
        }

        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints
            )
        {
            _validator.Validate(customerId, planCode, seatCount, paymentMethod);

            string normalizedPlanCode = planCode.Trim().ToUpperInvariant();
            string normalizedPaymentMethod = paymentMethod.Trim().ToUpperInvariant();

            var customer = _customerRepository.GetById(customerId);
            var plan = _planRepository.GetByCode(normalizedPlanCode);

            if (!customer.IsActive)
            {
                throw new InvalidOperationException("Inactive customers cannot renew subscriptions");
            }

            decimal baseAmount = (plan.MonthlyPricePerSeat * seatCount * 12m) + plan.SetupFee;
            string notes = string.Empty;

            decimal discountAmount = _discountCalculator.CalculateDiscount(customer, plan, seatCount, useLoyaltyPoints);
            notes += discountAmount;

            decimal subtotalAfterDiscount = baseAmount - discountAmount;
            if (subtotalAfterDiscount < 300m)
            {
                subtotalAfterDiscount = 300m;
                notes += "minimum discounted subtotal applied; ";
            }

            decimal supportFee = 0m;
            if (includePremiumSupport)
            {
                supportFee = _supportFeeCalculator.CalculateSupportFee(normalizedPlanCode);
                notes += "premium support included; ";
            }

            decimal paymentFeeBase = subtotalAfterDiscount + supportFee;
            decimal paymentFee = _paymentFeeCalculator.CalculatePaymentFee(paymentFeeBase, normalizedPaymentMethod);
            notes += _paymentFeeCalculator.GetMethod(normalizedPaymentMethod);

            decimal taxRate = _taxRateProvider.GetTaxRate(customer.Country);
            decimal taxBase = subtotalAfterDiscount + supportFee + paymentFee;
            decimal taxAmount = taxBase * taxRate;
            decimal finalAmount = taxBase + taxAmount;

            if (finalAmount < 500m)
            {
                finalAmount = 500m;
                notes += "minimum invoice amount applied; ";
            }

            var invoice = new RenewalInvoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customerId}-{normalizedPlanCode}",
                CustomerName = customer.FullName,
                PlanCode = normalizedPlanCode,
                PaymentMethod = normalizedPaymentMethod,
                SeatCount = seatCount,
                BaseAmount = Math.Round(baseAmount, 2, MidpointRounding.AwayFromZero),
                DiscountAmount = Math.Round(discountAmount, 2, MidpointRounding.AwayFromZero),
                SupportFee = Math.Round(supportFee, 2, MidpointRounding.AwayFromZero),
                PaymentFee = Math.Round(paymentFee, 2, MidpointRounding.AwayFromZero),
                TaxAmount = Math.Round(taxAmount, 2, MidpointRounding.AwayFromZero),
                FinalAmount = Math.Round(finalAmount, 2, MidpointRounding.AwayFromZero),
                Notes = notes.Trim(),
                GeneratedAt = DateTime.UtcNow
            };

            _billingGateway.saveInvoice(invoice);

            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                string subject = "Subscription renewal invoice";
                string body =
                    $"Hello {customer.FullName}, your renewal for plan {normalizedPlanCode} " +
                    $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

                _billingGateway.sendEmail(customer.Email, subject, body);
            }

            return invoice;
        }
    }
}
