using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Payments.Model;
using Payments.Processing;

namespace Payments.Processing.Implementation
{
    public class StripePaymentApiClient : IStripePaymentApiClient
    {
        private readonly string paymentApiBaseUrl;
        private readonly HttpClient httpClient;

        public StripePaymentApiClient(string paymentApiBaseUrl, HttpClient httpClient)
        {
            this.paymentApiBaseUrl = paymentApiBaseUrl;
            this.httpClient = httpClient;
        }

        public async Task<StripePayment> ChargePayment(int orderId, decimal amount)
        {
            StringContent body = new StringContent(
                            JsonConvert.SerializeObject(new { OrderId = orderId, Amount = amount }),
                            Encoding.UTF8,
                            "application/json");

            HttpResponseMessage httpResponse = await httpClient.PostAsync($"{paymentApiBaseUrl}/charge", body);
            JObject response = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new PaymentException(response["error"].ToString());
            }

            return response.ToObject<StripePayment>();
        }
    }
}
