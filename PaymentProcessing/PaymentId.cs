namespace PaymentProcessing
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

        public static implicit operator PaymentId(string value)
        {
            return new PaymentId(value);
        }
    }
}