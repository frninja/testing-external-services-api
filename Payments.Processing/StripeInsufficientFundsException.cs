using System;

namespace Payments.Processing
{
    public class StripeInsufficientFundsException : StripePaymentException
    {
        public StripeInsufficientFundsException() : base("Insufficient balance")
        {
        }
    }
}
