using System;
using System.Collections.Generic;
using System.Linq;

using PaymentProcessing;

namespace OrderProcessing
{
    public class Order
    {
        private List<string> paymentErrors = new List<string>();

        public int Id { get; }
        public decimal Total { get; }

        public bool IsPaid => PaymentId != null;
        public string PaymentId { get; private set; }

        public string LastPaymentError => paymentErrors.LastOrDefault();

        public Order(int id, decimal total)
        {
            Id = id;
            Total = total;
        }

        public void MarkAsPaid(IPayment payment)
        {
            PaymentId = payment.Id;
        }

        public void MarkAsPaid(StripePayment payment)
        {
            PaymentId = payment.TransactionId;
        }

        public void RecordPaymentError(string error)
        {
            paymentErrors.Add(error);
        }
    }
}