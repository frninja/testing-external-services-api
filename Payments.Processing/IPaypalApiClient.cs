using System.Threading.Tasks;

using Payments.Model;

namespace Payments.Processing
{
    public interface IPaypalApiClient
    {
        Task<PaypalOperationStatus> Pay(int orderId, decimal amount);
    }
}