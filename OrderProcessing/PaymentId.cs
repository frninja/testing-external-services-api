namespace OrderProcessing
{
    public class PaymentId
    {
        public string Value { get; }

        public PaymentId(string value)
        {
            Value = value;
        }

        public static implicit operator string(PaymentId id)
        {
            return id.Value;
        }
    }
}