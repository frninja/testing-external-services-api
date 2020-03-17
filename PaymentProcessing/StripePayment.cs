using System;
using Newtonsoft.Json;

namespace PaymentProcessing
{
    public class StripePayment
    {
        [JsonProperty(PropertyName = "transaction_id")]
        public string TransactionId { get; set; }
    }
}
