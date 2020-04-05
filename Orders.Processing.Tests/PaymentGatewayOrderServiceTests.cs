using System;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

using Orders.Model;
using Orders.Processing.Implementation;
using Payments.Processing;

namespace Orders.Processing.Tests
{
    [TestFixture]
    public class PaymentGatewayOrderServiceTests
    {
        [Test]
        public async Task ChargeOrder_WhenCustomerHasNotEnoughMoney_ShouldRecordLastPaymentError()
        {
            Order order = new Order(id: 1, total: 99.0m);

            IOrderPaymentGateway fakePaymentGateway = Substitute.For<IOrderPaymentGateway>();
            fakePaymentGateway.ChargeOrder(order).Throws<StripeInsufficientFundsException>();

            PaymentGatewayOrderService service = new PaymentGatewayOrderService(fakePaymentGateway);

            await service.ChargeOrder(order);

            Assert.AreEqual("Insufficient balance", order.LastPaymentError);
        }
    }
}
