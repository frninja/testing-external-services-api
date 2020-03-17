using System;
using System.Threading.Tasks;

namespace PaymentProcessing
{
    public class StripePaymentGateway : IPaymentGateway
    {
        private IStripePaymentApiClient apiClient;

        public StripePaymentGateway(IStripePaymentApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IPayment> ChargeOrder(int orderId, decimal amount)
        {
            StripePayment payment = await apiClient.ChargePayment(orderId, amount);
            return payment;
        }
    }
}
