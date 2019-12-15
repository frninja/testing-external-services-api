using System;

namespace OrderProcessing
{
    public class Order
    {
        public int Id { get; private set; }
        public decimal Total { get; private set; }

        public bool IsPaid { get; private set; }
        public string PaymentId { get; private set; }
        public string PaymentError { get; private set; }

        public Order(int id, decimal total)
        {
            Id = id;
            Total = total;
        }

        public void MarkAsPaid(string transactionId)
        {
            IsPaid = true;
            PaymentId = transactionId;
        }

        public void RecordPaymentError(string error)
        {
            PaymentError = error;
        }
    }
}