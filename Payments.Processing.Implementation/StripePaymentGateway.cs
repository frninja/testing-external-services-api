using System;
using System.Threading.Tasks;

using Orders.Model;
using Orders.Processing;

using Payments.Model;

namespace Payments.Processing
{
    public class StripePaymentGateway : IOrderPaymentGateway
    {
        private IStripePaymentApiClient apiClient;

        public StripePaymentGateway(IStripePaymentApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<Payment> ChargeOrder(Order order)
        {
            StripePayment payment = await apiClient.ChargePayment(order.Id, order.Total);
            return MapToPayment(payment);
        }

        private Payment MapToPayment(StripePayment payment)
        {
            return new Payment(payment.TransactionId);
        }
    }
}
