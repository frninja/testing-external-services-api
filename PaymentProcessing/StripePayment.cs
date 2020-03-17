using System;
using Newtonsoft.Json;

namespace PaymentProcessing
{
    public class StripePayment : IPayment
    {
        public string Id => TransactionId;

        [JsonProperty(PropertyName = "transaction_id")]
        public string TransactionId { get; set; }
    }
}
