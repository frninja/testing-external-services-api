using System;
using Newtonsoft.Json;

namespace Payments.Model
{
    public class StripePayment
    {
        [JsonProperty(PropertyName = "transaction_id")]
        public string TransactionId { get; set; }
    }
}
