using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using PaymentProcessing;

namespace OrderProcessing
{

    public class InjectedHttpOrderService : IOrderService
    {
        private readonly string paymentApiBaseUrl;
        private readonly HttpClient httpClient;

        public InjectedHttpOrderService(string paymentApiBaseUrl, HttpClient httpClient)
        {
            this.paymentApiBaseUrl = paymentApiBaseUrl;
            this.httpClient = httpClient;
        }

        public async Task ChargeOrder(Order order)
        {
            try
            {
                PaymentId transactionId = await ChargePayment(order.Id, order.Total);
                order.MarkAsPaid(transactionId);
            }
            catch (PaymentException e)
            {
                order.RecordPaymentError(e.Message);
            }
        }

        private async Task<PaymentId> ChargePayment(int orderId, decimal amount)
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

            return new PaymentId(response["transaction_id"].ToString());
        }
    }
}