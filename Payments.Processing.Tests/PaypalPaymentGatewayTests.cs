using System;
using System.Threading.Tasks;

using NUnit.Framework;
using NSubstitute;

using Orders.Model;
using Payments.Model;
using Payments.Processing.Implementation;

namespace Payments.Processing.Tests
{
    [TestFixture]
    public class PaypalPaymentGatewayTests
    {
        [Test]
        public async Task Charge_WhenOperationIsNotAuthorized_ShouldRetryPayment()
        {
            string paymentId = "777";
            Order order = new Order(id: 1, total: 99);

            IPaypalApiClient fakeApiClient = Substitute.For<IPaypalApiClient>();
            fakeApiClient.Pay(Arg.Any<int>(), Arg.Any<decimal>()).Returns(
                new PaypalOperationStatus { ErrorCode = PaypalErrorCode.UnauthorizedOperation },
                new PaypalOperationStatus { PaymentId = paymentId });
            PaypalPaymentGateway paymentGateway = new PaypalPaymentGateway(fakeApiClient);


            Payment payment = await paymentGateway.ChargeOrder(order);

            Assert.AreEqual(paymentId, payment.ExternalPaymentId);
        }
    }
}
