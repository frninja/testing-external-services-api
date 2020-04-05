using System;
using System.Threading.Tasks;
using Orders.Model;
using Orders.Processing;
using Payments.Model;
using Polly;

namespace Payments.Processing.Implementation
{
    public class PaypalPaymentGateway : IPaymentGateway
    {
        private readonly IPaypalApiClient apiClient;

        public PaypalPaymentGateway(IPaypalApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<Payment> ChargeOrder(Order order)
        {
            PaypalOperationStatus paymentOperationStatus = await Policy
                .HandleResult<PaypalOperationStatus>(status => status.ErrorCode == PaypalErrorCode.UnauthorizedOperation)
                .RetryAsync(10)
                .ExecuteAsync(async () => await apiClient.Pay(order.Id, order.Total));

            if (paymentOperationStatus.PaymentId != null)
            {
                return MapToPayment(paymentOperationStatus);
            }
            else if (paymentOperationStatus.ErrorCode == PaypalErrorCode.InsufficientBalance)
            {
                throw new NotEnoughMoneyException("Sorry, not enough money");
            }
            else 
            {
                throw new PaymentException($"Payment failed. Internal error: {paymentOperationStatus.ErrorCode}");
            }
        }

        private Payment MapToPayment(PaypalOperationStatus status)
        {
            return new Payment(status.PaymentId);
        }
    }
}
