using System;
namespace PaymentProcessing
{
    public class InsufficientFundsException : PaymentException
    {
        public InsufficientFundsException() : base("Insufficient balance")
        {
        }
    }
}
