using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;
using NUnit.Framework;

namespace OrderProcessing.Tests
{
    [TestFixture]
    public class OrderProcessingTests
    {
        private const string PaymentApiBaseUrl = "https://api.payment-service.com";

        [Test]
        public async Task ChargeOrder_WhenCustomerHasNotEnoughMoney_ShouldRecordLastPaymentError()
        {
            Order order = new Order(id: 1, total: 99.0m);
            HttpClient fakeHttpClient = new HttpClient(
                FakeHttpResponse(
                    new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri($"{PaymentApiBaseUrl}/charge")
                    },
                    new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        Content = new StringContent(JsonConvert.SerializeObject(new
                        {
                            success = false,
                            error = "Insufficient balance",
                        }))
                    })
            );
            OrderService service = new OrderService(PaymentApiBaseUrl, fakeHttpClient);

            await service.ChargeOrder(order);

            Assert.AreEqual("Insufficient balance", order.PaymentError);
        }

        private static HttpMessageHandler FakeHttpResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            return new FakeHttpMessageHandler(request, response);
        }

        private class FakeHttpMessageHandler : HttpMessageHandler
        {
            private readonly HttpRequestMessage request;
            private readonly HttpResponseMessage response;

            public FakeHttpMessageHandler(HttpRequestMessage request, HttpResponseMessage response)
            {
                this.request = request;
                this.response = response;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                bool requestMatched = this.request.Method == request.Method && this.request.RequestUri == request.RequestUri;
                if (!requestMatched)
                {
                    throw new Exception($"Unexpected request: {request.RequestUri}");
                }

                return Task.FromResult(response);
            }
        }
    }
}
