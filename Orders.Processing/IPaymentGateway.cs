using System;
using System.Threading.Tasks;

using Orders.Model;
using Payments.Model;

namespace Orders.Processing
{
    public interface IPaymentGateway
    {
        Task<Payment> ChargeOrder(Order order);
    }
}
