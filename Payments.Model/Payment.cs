using System;

namespace Payments.Model
{
    public class Payment
    {
        public string ExternalPaymentId { get; }

        public Payment(string id)
        {
            ExternalPaymentId = id;
        }
    }
}
