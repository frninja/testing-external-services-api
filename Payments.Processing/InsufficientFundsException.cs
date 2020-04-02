using System;

namespace Payments.Processing
{
    public class InsufficientFundsException : PaymentException
    {
        public InsufficientFundsException() : base("Insufficient balance")
        {
        }
    }
}
