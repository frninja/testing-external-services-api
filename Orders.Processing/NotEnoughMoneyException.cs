using System;
using System.Runtime.Serialization;

namespace Orders.Processing
{
    public class NotEnoughMoneyException : PaymentException
    {
        public NotEnoughMoneyException()
        {
        }

        public NotEnoughMoneyException(string message) : base(message)
        {
        }

        public NotEnoughMoneyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}