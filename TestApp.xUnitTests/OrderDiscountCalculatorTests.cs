using FluentAssertions;
using FluentAssertions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals;
using Xunit;

namespace TestApp.xUnitTests
{
    // FluentAssertions

    // Happy Hours - w godz. 9:00 - 16:30 klient otrzymuje 10% upustu
    public class OrderDiscountCalculatorTests
    {
        private OrderDiscountCalculator calculator;

        private readonly DateTime beginHour = DateTime.MinValue.AddHours(9);
        private readonly DateTime endHour = DateTime.MinValue.AddHours(16).AddMinutes(30);

        const decimal discount = 0.1m;

        public OrderDiscountCalculatorTests()
        {
            calculator = new OrderDiscountCalculator(beginHour.TimeOfDay, endHour.TimeOfDay, discount);
        }

        [Fact]
        public void CalculateDiscount_BeforeHappyHour_ShouldNotReturnsDiscount()
        {
            // Arrange
            Order order = new Order { OrderDate = beginHour.AddMinutes(-1), TotalAmount = 100 };

            // Act
            decimal result = calculator.CalculateDiscount(order);

            // Assert
            // Assert.Equal(expected: 0, result);

            result.Should().Be(0);

        }

        [Fact]
        public void CalculateDiscount_AfterHappyHour_ShouldNotReturnsDiscount()
        {
            // Arrange
            Order order = new Order { OrderDate = endHour.AddMinutes(1), TotalAmount = 100 };

            // Act
            decimal result = calculator.CalculateDiscount(order);

            // Assert
            // Assert.Equal(expected: 0, result);

            result.Should().Be(0);
            
        }

        [Fact]
        public void CalculateDiscount_DuringBeginHappyHours_ShouldReturnsDiscount()
        {
            Order order = new Order { OrderDate = beginHour, TotalAmount = 100 };

            // Act
            decimal result = calculator.CalculateDiscount(order);

            // Assert
            // Assert.Equal(expected: 10, result);

            result.Should().Be(10);
        }

        [Fact]
        public void CalculateDiscount_DuringEndHappyHours_ShouldReturnsDiscount()
        {
            Order order = new Order { OrderDate = endHour, TotalAmount = 100 };

            // Act
            decimal result = calculator.CalculateDiscount(order);

            // Assert
            // Assert.Equal(expected: 10, result);

            result.Should().Be(10);
        }

        [Fact]
        public void CalculateDiscount_EmptyOrder_ShouldArgumentNullException()
        {
            // Act
            Action act = ()=>calculator.CalculateDiscount(null);

            // Assert
            // Assert.Throws<ArgumentNullException>(act);

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("order");

        }

        [Fact]
        public void CalculateDiscount_ValidOrder_ShouldExecutionTimeIsBelow200ms()
        {
            // Act
            Action act = () => calculator.CalculateDiscount(new Order());

            // Assert
            act.ExecutionTime().Should().BeLessOrEqualTo(200.Milliseconds());
        }
    }
}
