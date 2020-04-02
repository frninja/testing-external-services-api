using System;
using System.Runtime.Serialization;

namespace Payments.Processing
{
    public class PaymentException : Exception
    {
        public PaymentException(string message) : base(message)
        {
        }

        public PaymentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}