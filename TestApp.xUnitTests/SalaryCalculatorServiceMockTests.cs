using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Mocking;
using Xunit;

namespace TestApp.xUnitTests
{
    public class SalaryCalculatorServiceLinqToMockTests
    {

    }


    public class SalaryCalculatorServiceMockTests
    {
        private Mock<IRateService> mockRateService;
        private SalaryCalculatorService salaryCalculatorService;

        public SalaryCalculatorServiceMockTests()
        {
            mockRateService = new Mock<IRateService>();
            salaryCalculatorService = new SalaryCalculatorService(mockRateService.Object);
        }

        [Theory]
        [InlineData(100, "", 1f, 100)]
        [InlineData(100, "PLN", 1f, 100)]
        [InlineData(100, "EUR", 4.01f, 401)]
        [InlineData(100, "USD", 4.00f, 400)]
        [InlineData(100, "CHF", 4.15f, 415)]
        public async Task CalculateAsync_AmountCurrency_ShouldReturnsAmountByCurrencyRatioAsync(decimal amount, string currencyCode, float ratio, decimal expected)
        {
            // Arrange
            mockRateService.Setup(rs => rs.GetAsync(currencyCode))
                .ReturnsAsync(new Rate { code = currencyCode, mid = ratio });

            // Act
            decimal result = await salaryCalculatorService.CalculateAsync(amount, currencyCode);

            // Assert
            Assert.Equal(expected: expected, result);
        }

        [Fact]
        public async Task CalculateAsync_AmountPLN_ShouldNotCallGetAsync()
        {
            // Arrange
            mockRateService
                .Setup(rt => rt.GetAsync("PLN"))
                .Verifiable();

            // Act
            await salaryCalculatorService.CalculateAsync(100, "PLN");

            // Assert
            mockRateService.Verify(rt => rt.GetAsync("PLN"), Times.Never);
        }

        [Fact]
        public async Task CalculateAsync_AmountUSD_ShouldCallGetAsync()
        {
            // Arrange
            mockRateService
                .Setup(rt => rt.GetAsync("USD"))
                .ReturnsAsync(new Rate { code = "USD", mid = 4.00f })
                .Verifiable();

            // Act
            await salaryCalculatorService.CalculateAsync(100, "USD");

            // Assert
            mockRateService.Verify(rt => rt.GetAsync("USD"), Times.Once);
        }




        /*
         

        [Fact]
        public async Task CalculateAsync_AmountPLN_ShouldReturnsTheSameAmountAsync()
        {
            // Arrange

            // Act
            decimal result = await salaryCalculatorService.CalculateAsync(100, "PLN");

            // Assert
            Assert.Equal(expected: 100, result);
        }

        [Fact]
        public async Task CalculateAsync_AmountEUR_ShouldReturnsAmountByEURRatioAsync()
        {
            // Arrange
            mockRateService.Setup(rs => rs.GetAsync("EUR"))
                .ReturnsAsync(new Rate { code = "EUR", mid = 4.01f });

            // Act
            decimal result = await salaryCalculatorService.CalculateAsync(100, "EUR");

            // Assert
            Assert.Equal(expected: 401, result);
        }

        [Fact]
        public async Task CalculateAsync_AmountUSD_ShouldReturnsAmountByUSDRatioAsync()
        {
            mockRateService.Setup(rs => rs.GetAsync("USD"))
                .ReturnsAsync(new Rate { code = "USD", mid = 4.00f });

            // Act
            decimal result = await salaryCalculatorService.CalculateAsync(100, "USD");

            // Assert
            Assert.Equal(expected: 400, result);
        }

        */
    }
}
