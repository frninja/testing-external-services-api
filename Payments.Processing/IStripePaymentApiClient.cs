using System.Threading.Tasks;

using Payments.Model;

namespace Payments.Processing
{
    public interface IStripePaymentApiClient
    {
        Task<StripePayment> ChargePayment(int orderId, decimal amount);
    }
}