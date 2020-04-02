using System;
using System.Threading.Tasks;

using NSubstitute;
using NUnit.Framework;

using Orders.Model;
using Orders.Processing.Implementation;
using Payments.Model;
using Payments.Processing;

namespace OrderProcessing.Tests
{
    [TestFixture]
    public class StripePaymentApiOrderServiceTests
    {
        [Test]
        public async Task ChargeOrder_WhenPaymentIsProcessed_ShouldMarkOrderAsPaid()
        {
            Order order = new Order(id: 1, total: 99.0m);

            IStripePaymentApiClient fakePaymentApiClient = Substitute.For<IStripePaymentApiClient>();
            fakePaymentApiClient.ChargePayment(orderId: order.Id, amount: order.Total).Returns(new StripePayment
            {
                TransactionId = "777"
            });

            StripePaymentApiOrderService service = new StripePaymentApiOrderService(fakePaymentApiClient);

            await service.ChargeOrder(order);

            Assert.IsTrue(order.IsPaid);
        }
    }
}
