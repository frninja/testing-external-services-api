﻿using System;
using System.Runtime.Serialization;

namespace PaymentProcessing
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