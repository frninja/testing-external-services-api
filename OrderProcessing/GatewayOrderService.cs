using System;
using System.Threading.Tasks;
using PaymentProcessing;

namespace OrderProcessing
{
    public class GatewayOrderService : IOrderService
    {
        private IPaymentGateway paymentGateway;

        public GatewayOrderService(IPaymentGateway paymentGateway)
        {
            this.paymentGateway = paymentGateway;
        }

        public async Task ChargeOrder(Order order)
        {
            PaymentResult paymentResult = await paymentGateway.ChargePayment(order.Id, order.Total);
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
