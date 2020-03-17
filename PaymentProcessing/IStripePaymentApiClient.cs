using System.Threading.Tasks;

namespace PaymentProcessing
{
    public interface IStripePaymentApiClient
    {
        Task<StripePayment> ChargePayment(int orderId, decimal amount);
    }
}