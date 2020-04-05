using System;
using System.Runtime.Serialization;

namespace Orders.Processing
{
    public class PaymentException : Exception
    {
        public PaymentException()
        {
        }

        public PaymentException(string message) : base(message)
        {
        }

        public PaymentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}