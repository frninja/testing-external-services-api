using System;
using System.Threading.Tasks;
using PaymentProcessing;

namespace OrderProcessing
{
    public class PaymentApiOrderService : IOrderService
    {
        private IPaymentApiClient paymentApiClient;

        public PaymentApiOrderService(IPaymentApiClient paymentApiClient)
        {
            this.paymentApiClient = paymentApiClient;
        }

        public async Task ChargeOrder(Order order)
        {
            PaymentResult paymentResult = await paymentApiClient.ChargePayment(order.Id, order.Total);
            if (paymentResult.Success)
            {
                order.MarkAsPaid(paymentResult.TransactionId);
            }
            else
            {
                order.RecordPaymentError(paymentResult.Error);
            }
        }
    }
}
