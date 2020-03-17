using System;
using System.Threading.Tasks;

namespace PaymentProcessing
{
    public interface IPaymentGateway
    {
        Task<IPayment> ChargeOrder(int orderId, decimal amount);
    }
}
