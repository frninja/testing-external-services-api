using System;
using System.Collections.Generic;
using System.Linq;

namespace Orders.Model
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

        public void MarkAsPaid(string paymentId)
        {
            PaymentId = paymentId;
        }

        public void RecordPaymentError(string error)
        {
            paymentErrors.Add(error);
        }
    }
}