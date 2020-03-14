using System;
using System.Threading.Tasks;

namespace PaymentProcessing
{
    public interface IPaymentGateway
    {
        Task<PaymentId> ChargeOrder(int orderId, decimal amount);
    }
}
