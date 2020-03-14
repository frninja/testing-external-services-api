using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderProcessing
{
    public class Order
    {
        private List<string> paymentErrors = new List<string>();

        public OrderId Id { get; }
        public decimal Total { get; }

        public bool IsPaid => PaymentId != null;
        public PaymentId PaymentId { get; private set; }

        public string LastPaymentError => paymentErrors.LastOrDefault();

        public Order(OrderId id, decimal total)
        {
            Id = id;
            Total = total;
        }

        public void MarkAsPaid(string transactionId)
        {
            PaymentId = new PaymentId(transactionId);
        }

        public void RecordPaymentError(string error)
        {
            paymentErrors.Add(error);
        }
    }
}