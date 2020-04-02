using System;
using System.Threading.Tasks;

using Orders.Model;
using Payments.Model;
using Payments.Processing;

namespace Orders.Processing.Implementation
{
    public class PaymentGatewayOrderService : IOrderService
    {
        private IOrderPaymentGateway paymentGateway;

        public PaymentGatewayOrderService(IOrderPaymentGateway paymentGateway)
        {
            this.paymentGateway = paymentGateway;
        }

        public async Task ChargeOrder(Order order)
        {
            try
            {
                Payment payment = await paymentGateway.ChargeOrder(order);
                order.MarkAsPaid(payment.Id);
            }
            catch (PaymentException e)
            {
                order.RecordPaymentError(e.Message);
            }
        }
    }
}
