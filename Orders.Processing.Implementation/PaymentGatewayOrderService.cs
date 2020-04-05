using System;
using System.Threading.Tasks;

using Orders.Model;
using Payments.Model;
using Payments.Processing;

namespace Orders.Processing.Implementation
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
                Payment payment = await paymentGateway.ChargeOrder(order);
                order.MarkAsPaid(payment.ExternalPaymentId);
            }
            catch (NotEnoughMoneyException)
            {
                order.RecordPaymentError("Not enough money");
            }
            catch (PaymentException e)
            {
                order.RecordPaymentError(e.Message);
            }
        }
    }
}
