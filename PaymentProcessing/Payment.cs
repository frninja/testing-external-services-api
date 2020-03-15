using System;
using Newtonsoft.Json;

namespace PaymentProcessing
{
    public class Payment
    {
        [JsonProperty(PropertyName = "transaction_id")]
        public string TransactionId { get; set; }
    }
}
