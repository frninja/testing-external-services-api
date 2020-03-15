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
            try
            {
                Payment payment = await paymentApiClient.ChargePayment(order.Id, order.Total);
                order.MarkAsPaid(payment);
            }
            catch (PaymentException e)
            {
                order.RecordPaymentError(e.Message);
            }
        }
    }
}
