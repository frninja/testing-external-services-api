namespace OrderProcessing
{
    public class TransactionId
    {
        public string Value { get; }

        public TransactionId(string value)
        {
            Value = value;
        }

        public static implicit operator string(TransactionId transactionId)
        {
            return transactionId.Value;
        }
    }
}