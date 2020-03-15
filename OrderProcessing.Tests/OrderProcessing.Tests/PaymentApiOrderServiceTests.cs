using System;
using System.Threading.Tasks;

using NSubstitute;
using NUnit.Framework;

using PaymentProcessing;

namespace OrderProcessing.Tests
{
    [TestFixture]
    public class PaymentApiOrderServiceTests
    {
        [Test]
        public async Task ChargeOrder_WhenPaymentIsProcessed_ShouldMarkOrderAsPaid()
        {
            Order order = new Order(id: 1, total: 99.0m);

            IPaymentApiClient fakePaymentApiClient = Substitute.For<IPaymentApiClient>();
            fakePaymentApiClient.ChargePayment(orderId: order.Id, amount: order.Total).Returns(new Payment
            {
                TransactionId = "777"
            });

            PaymentApiOrderService service = new PaymentApiOrderService(fakePaymentApiClient);

            await service.ChargeOrder(order);

            Assert.IsTrue(order.IsPaid);
        }
    }
}
