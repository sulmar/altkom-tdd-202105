using Moq;
using System;
using System.Threading.Tasks;
using TestApp.Mocking;
using Xunit;

namespace TestApp.xUnitTests
{
    public class SalaryCalculatorServiceLinqToMockTests
    {
        // TODO: Napisz testy z użyciem Mock.Of<T>

        [Theory]
        [InlineData(100, "", 1f, 100)]
        [InlineData(100, "PLN", 1f, 100)]
        [InlineData(100, "EUR", 4.01f, 401)]
        [InlineData(100, "USD", 4.00f, 400)]
        [InlineData(100, "CHF", 4.15f, 415)]
        public async Task CalculateAsync_AmountCurrency_ShouldReturnsAmountByCurrencyRatioAsync(decimal amount, string currencyCode, float ratio, decimal expected)
        {
            // Arrange
            IRateService rateService = Mock.Of<IRateService>(
                rs => rs.GetAsync(currencyCode) == Task.FromResult(new Rate { code = currencyCode, mid = ratio }));

            SalaryCalculatorService salaryCalculatorService = new SalaryCalculatorService(rateService);

            // Act
            decimal result = await salaryCalculatorService.CalculateAsync(amount, currencyCode);

            // Assert
            Assert.Equal(expected: expected, result);
        }

        [Fact]
        public async Task CalculateAsync_AmountPLN_ShouldNotCallGetAsync()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task CalculateAsync_AmountUSD_ShouldCallGetAsync()
        {
            IRateService rateService = Mock.Of<IRateService>(
                rs => rs.GetAsync("USD") == Task.FromResult(new Rate { code = "USD", mid = 4.00f } ));

            throw new NotImplementedException();

        }
    }
}
