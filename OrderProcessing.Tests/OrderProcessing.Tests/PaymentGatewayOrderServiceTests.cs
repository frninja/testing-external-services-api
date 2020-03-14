using System;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using PaymentProcessing;

namespace OrderProcessing.Tests
{
    [TestFixture]
    public class PaymentGatewayOrderServiceTests
    {
        [Test]
        public async Task ChargeOrder_WhenCustomerHasNotEnoughMoney_ShouldRecordLastPaymentError()
        {
            Order order = new Order(id: 1, total: 99.0m);

            IPaymentGateway fakePaymentGateway = Substitute.For<IPaymentGateway>();
            fakePaymentGateway.ChargeOrder(orderId: order.Id, amount: order.Total).Throws<InsufficientFundsException>();

            PaymentGatewayOrderService service = new PaymentGatewayOrderService(fakePaymentGateway);

            await service.ChargeOrder(order);

            Assert.AreEqual("Insufficient balance", order.LastPaymentError);
        }
    }
}
