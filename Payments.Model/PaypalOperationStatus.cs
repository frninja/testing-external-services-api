namespace Payments.Model
{
    public class PaypalOperationStatus
    {
        public string PaymentId { get; set; }
        public PaypalErrorCode ErrorCode { get; set; }
    }
}