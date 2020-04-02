using System.Threading.Tasks;

using Orders.Model;

namespace Orders.Processing.Implementation
{
    public interface IOrderService
    {
        Task ChargeOrder(Order order);
    }
}