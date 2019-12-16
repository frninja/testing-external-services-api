using System;
using System.Threading.Tasks;

using NSubstitute;
using NUnit.Framework;

using PaymentProcessing;

namespace OrderProcessing.Tests
{
    [TestFixture]
    public class GatewayOrderServiceTests
    {
        [Test]
        public async Task ChargeOrder_WhenPaymentIsProcessed_ShouldMarkOrderAsPaid()
        {
            Order order = new Order(id: 1, total: 99.0m);
            IPaymentGateway paymentGateway = Substitute.For<IPaymentGateway>();
            paymentGateway.ChargePayment(orderId: order.Id, amount: order.Total).Returns(PaymentResult.Ok(transactionId: "777"));
            GatewayOrderService service = new GatewayOrderService(paymentGateway);

            await service.ChargeOrder(order);

            Assert.IsTrue(order.IsPaid);
        }
    }
}
