using System;
using System.Runtime.Serialization;

namespace OrderProcessing
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