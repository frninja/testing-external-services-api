using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PaymentProcessing
{
    public class StripePaymentApiClient : IPaymentApiClient
    {
        private readonly string paymentApiBaseUrl;
        private readonly HttpClient httpClient;

        public StripePaymentApiClient(string paymentApiBaseUrl, HttpClient httpClient)
        {
            this.paymentApiBaseUrl = paymentApiBaseUrl;
            this.httpClient = httpClient;
        }

        public async Task<PaymentResult> ChargePayment(int orderId, decimal amount)
        {
            StringContent body = new StringContent(
                            JsonConvert.SerializeObject(new { OrderId = orderId, Amount = amount }),
                            Encoding.UTF8,
                            "application/json");

            HttpResponseMessage httpResponse = await httpClient.PostAsync($"{paymentApiBaseUrl}/charge", body);
            JObject response = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

            if (!httpResponse.IsSuccessStatusCode)
            {
                return PaymentResult.Failed(response["error"].ToString());
            }

            return PaymentResult.Ok(response["transaction_id"].ToString());
        }
    }
}
