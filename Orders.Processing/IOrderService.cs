using System.Threading.Tasks;

using Orders.Model;

namespace Orders.Processing
{
    public interface IOrderService
    {
        Task ChargeOrder(Order order);
    }
}