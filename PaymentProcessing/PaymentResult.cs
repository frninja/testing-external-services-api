using System;

namespace PaymentProcessing
{
    public class PaymentResult
    {
        public bool Success { get; private set; }
        public string TransactionId { get; private set; }
        public string Error { get; private set; }

        public static PaymentResult Ok(string transactionId)
        {
            return new PaymentResult
            {
                Success = true,
                TransactionId = transactionId
            };
        }

        public static PaymentResult Failed(string error)
        {
            return new PaymentResult
            {
                Success = false,
                Error = error,
            };
        }
    }
}