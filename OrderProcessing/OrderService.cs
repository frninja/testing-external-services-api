using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderProcessing
{

    public class OrderService
    {
        private readonly string paymentApiBaseUrl = "https://api.payment-service.com";
        private readonly HttpClient httpClient;

        public OrderService(string paymentApiBaseUrl)
        {
            this.paymentApiBaseUrl = paymentApiBaseUrl;
            this.httpClient = new HttpClient();
        }

        public OrderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task ChargeOrder(Order order)
        {
            try
            {
                string transcationId = await ChargePayment(order.Id, order.Total);
                order.MarkAsPaid(transcationId);
            }
            catch (PaymentException e)
            {
                order.RecordPaymentError(e.PaymentError);
            }
        }

        private async Task<string> ChargePayment(int orderId, decimal amount)
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

            return response["transaction_id"].ToString();
        }
    }
}