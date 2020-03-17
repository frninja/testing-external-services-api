using System;
using System.Threading.Tasks;
using PaymentProcessing;

namespace OrderProcessing
{
    public class PaymentGatewayOrderService : IOrderService
    {
        private IPaymentGateway paymentGateway;

        public PaymentGatewayOrderService(IPaymentGateway paymentGateway)
        {
            this.paymentGateway = paymentGateway;
        }

        public async Task ChargeOrder(Order order)
        {
            try
            {
                IPayment payment = await paymentGateway.ChargeOrder(order.Id, order.Total);
                order.MarkAsPaid(payment);
            }
            catch (PaymentException e)
            {
                order.RecordPaymentError(e.Message);
            }
        }
    }
}
