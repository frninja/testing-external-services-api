using System;
using System.Threading.Tasks;

using Orders.Model;
using Payments.Model;
using Payments.Processing;

namespace Orders.Processing.Implementation
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
                order.MarkAsPaid(payment.TransactionId);
            }
            catch (PaymentException e)
            {
                order.RecordPaymentError(e.Message);
            }
        }
    }
}
