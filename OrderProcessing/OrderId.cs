using System;

namespace OrderProcessing
{
    public class OrderId
    {
        public int Value { get; }

        public OrderId(int value)
        {
            Value = value;
        }

        public static implicit operator int(OrderId id)
        {
            return id.Value;
        }

        public static implicit operator OrderId(int value)
        {
            return new OrderId(value);
        }
    }
}
