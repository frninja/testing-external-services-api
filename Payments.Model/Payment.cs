using System;

namespace Payments.Model
{
    public class Payment
    {
        public string Id { get; }

        public Payment(string id)
        {
            Id = id;
        }
    }
}
