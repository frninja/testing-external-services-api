using System;
using System.Net;
using System.Threading.Tasks;

using Newtonsoft.Json;
using NUnit.Framework;

using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace OrderProcessing.Tests
{
    [TestFixture]
    public class WiredHttpOrderServiceTests
    {
        private FluentMockServer server;
        private readonly static string PaymentApiBaseUrl = "http://localhost:9000";

        [SetUp]
        public void SetUp()
        {
            server = FluentMockServer.Start(new FluentMockServerSettings
            {
                Urls = new string[] { PaymentApiBaseUrl }
            });

            server
                .Given(
                    Request.Create()
                        .WithPath("/charge")
                        .WithBody(JsonConvert.SerializeObject(new
                        {
                            OrderId = 1,
                            Amount = 99.0m,
                        }))
                        .UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithHeader("Content-Type", "application/json")
                        .WithBodyAsJson(new
                        {
                            success = true,
                            transaction_id = 777,
                        }));
        }

        [Test]
        public async Task ChargeOrder_WhenPaymentProcessed_ShouldMarkOrderAsPaid()
        {
            Order order = new Order(id: 1, total: 99.0m);
            WiredHttpOrderService service = new WiredHttpOrderService(PaymentApiBaseUrl);

            await service.ChargeOrder(order);

            Assert.IsTrue(order.IsPaid);
        }

        [TearDown]
        public void TearDown()
        {
            server.Stop();
        }
    }
}
