using System.Threading.Tasks;

namespace PaymentProcessing
{
    public interface IPaymentApiClient
    {
        Task<PaymentResult> ChargePayment(int orderId, decimal amount);
    }
}