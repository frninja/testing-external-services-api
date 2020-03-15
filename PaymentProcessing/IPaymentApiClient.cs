using System.Threading.Tasks;

namespace PaymentProcessing
{
    public interface IPaymentApiClient
    {
        Task<Payment> ChargePayment(int orderId, decimal amount);
    }
}