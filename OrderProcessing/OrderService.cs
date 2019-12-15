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
        private string paymentApiBaseUrl;

        public OrderService(string paymentApiBaseUrl)
        {
            this.paymentApiBaseUrl = paymentApiBaseUrl;
        }

        public async Task ChargeOrder(Order order)
        {
            string transcationId = await ChargePayment(order.Id, order.Total);

            order.MarkAsPaid(transcationId);
        }

        private async Task<string> ChargePayment(int orderId, decimal amount)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent body = new StringContent(
                    JsonConvert.SerializeObject(new { OrderId = orderId, Amount = amount }),
                    Encoding.UTF8,
                    "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync($"{paymentApiBaseUrl}/charge", body);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Payment failed");
                }

                string json = await httpResponse.Content.ReadAsStringAsync();
                JObject responseObject = JObject.Parse(json);

                return responseObject["transaction_id"].ToString();
            }
        }
    }
}