using System.Threading.Tasks;

namespace OrderProcessing
{
    public interface IOrderService
    {
        Task ChargeOrder(Order order);
    }
}