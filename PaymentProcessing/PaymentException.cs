using System;
using System.Runtime.Serialization;

namespace PaymentProcessing
{
    public class PaymentException : Exception
    {
        public string PaymentError { get; private set; }

        public PaymentException(string message) : base(message)
        {
            PaymentError = message;
        }
    }
}