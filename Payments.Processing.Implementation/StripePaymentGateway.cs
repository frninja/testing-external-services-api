﻿using System;
using System.Threading.Tasks;

using Orders.Model;
using Orders.Processing;

using Payments.Model;

namespace Payments.Processing
{
    public class StripePaymentGateway : IPaymentGateway
    {
        private IStripePaymentApiClient apiClient;

        public StripePaymentGateway(IStripePaymentApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<Payment> ChargeOrder(Order order)
        {
            try
            {
                StripePayment payment = await apiClient.ChargePayment(order.Id, order.Total);
                return MapToPayment(payment);
            }
            catch (StripeInsufficientFundsException e)
            {
                throw new NotEnoughMoneyException("Not enough money", e);
            }
            catch (StripePaymentException e)
            {
                throw new PaymentException("Sorry, payment failed", e);
            }
        }

        private Payment MapToPayment(StripePayment payment)
        {
            return new Payment(payment.TransactionId);
        }
    }
}
