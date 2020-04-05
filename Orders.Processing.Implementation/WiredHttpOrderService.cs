using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Orders.Model;
using Payments.Model;
using Payments.Processing;

namespace Orders.Processing.Implementation
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
                order.MarkAsPaid(payment.TransactionId);
            }
            catch (StripePaymentException e)
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
                    throw new StripePaymentException(response["error"].ToString());
                }

                return response.ToObject<StripePayment>();
            } 
        }
    }
}
