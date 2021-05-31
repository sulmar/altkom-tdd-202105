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

    public class OrderDiscountCalculator
    {
        public decimal CalculateDiscount(Order order)
        {
            throw new NotImplementedException();       
        }
    }
}
