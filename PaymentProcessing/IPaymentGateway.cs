using System.Threading.Tasks;

namespace PaymentProcessing
{
    public interface IPaymentGateway
    {
        Task<PaymentResult> ChargePayment(int orderId, decimal amount);
    }
}