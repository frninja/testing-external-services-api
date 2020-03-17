using System;
using System.Threading.Tasks;
using PaymentProcessing;

namespace OrderProcessing
{
    public class StripePaymentApiOrderService : IOrderService
    {
        private IStripePaymentApiClient paymentApiClient;

        public StripePaymentApiOrderService(IStripePaymentApiClient paymentApiClient)
        {
            this.paymentApiClient = paymentApiClient;
        }

        public async Task ChargeOrder(Order order)
        {
            try
            {
                StripePayment payment = await paymentApiClient.ChargePayment(order.Id, order.Total);
                order.MarkAsPaid(payment);
            }
            catch (PaymentException e)
            {
                order.RecordPaymentError(e.Message);
            }
        }
    }
}
