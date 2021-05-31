using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Fundamentals
{
    public class Order
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

    // Happy Hours - w godz. 9:00 - 16:00 klient otrzymuje 10% upustu

    // Napisać implementację oraz testy

    public class OrderDiscountCalculator
    {
        private readonly TimeSpan begin;
        private readonly TimeSpan end;

        private readonly decimal percentage;

        public OrderDiscountCalculator(TimeSpan begin, TimeSpan end, decimal percentage)
        {
            this.begin = begin;
            this.end = end;
            this.percentage = percentage;
        }

        public decimal CalculateDiscount(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (order.OrderDate.TimeOfDay >= begin && order.OrderDate.TimeOfDay<=end)
            {
                return order.TotalAmount * percentage;
            }
            else
            {
                return 0;
            }
        }
    }
}
