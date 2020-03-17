using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using PaymentProcessing;

namespace OrderProcessing
{
    public class WiredHttpOrderService : IOrderService
    {
        private readonly string paymentApiBaseUrl;

        public WiredHttpOrderService(string paymentApiBaseUrl)
        {
            this.paymentApiBaseUrl = paymentApiBaseUrl;
        }

        public async Task ChargeOrder(Order order)
        {
            StripePayment payment = await ChargePayment(order.Id, order.Total);
            try
            {
                order.MarkAsPaid(payment);
            }
            catch (PaymentException e)
            {
                order.RecordPaymentError(e.Message);
            }
        }

        private async Task<StripePayment> ChargePayment(int orderId, decimal amount)
        {
            using (HttpClient httpClient = new HttpClient())
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
}
