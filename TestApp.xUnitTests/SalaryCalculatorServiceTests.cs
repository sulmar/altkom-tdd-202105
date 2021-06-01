using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Mocking;
using Xunit;

namespace TestApp.xUnitTests
{
    public class FakeEURRateServiceValid : IRateService
    {
        public Task<Rate> GetAsync(string currencyCode)
        {
            return Task.FromResult(new Rate { code = "EUR", mid = 4.01f });
        }
    }

    public class FakeUSDRateServiceValid : IRateService
    {
        public Task<Rate> GetAsync(string currencyCode)
        {
            return Task.FromResult(new Rate { code = "USD", mid = 4.00f });
        }
    }

    public class FakeCHFRateServiceValid : IRateService
    {
        public Task<Rate> GetAsync(string currencyCode)
        {
            return Task.FromResult(new Rate { code = "CHF", mid = 4.15f });
        }
    }

    public class FakePLNRateService : IRateService
    {
        public Task<Rate> GetAsync(string currencyCode)
        {
            return null;
        }
    }

    public class FakeValidHttpClient : IHttpClient
    {
        public Task<RatesList[]> GetFromJsonAsync<T>(string url)
        {
            return Task.FromResult(GetFromJson<T>(url));
        }

        private RatesList[] GetFromJson<T>(string url)
        {
            return new RatesList[]
            {
                new RatesList
                {
                    rates = new Rate[]
                    {
                        new Rate { code = "EUR", mid = 4.01f },
                        new Rate { code = "CHF", mid = 4.15f },
                        new Rate { code = "USD", mid = 4.00f },
                    }
                }
            };
        }
    }


    public class NbpRateServiceTests
    {
        [Fact]
        public async Task GetAsync_USD_ShouldReturnsUSDRateAsync()
        {
            // Arrange
            IHttpClient client = new FakeValidHttpClient();
            IRateService rateService = new NbpRateService(client);

            // Act
            Rate rate = await rateService.GetAsync("USD");

            // Assert
            Assert.Equal(expected: "USD", rate.code);
            Assert.Equal(expected: 4.00, rate.mid);

        }

        [Fact]
        public async Task GetAsync_PLN_ShouldReturnsNull()
        {
            // Arrange
            IHttpClient client = new FakeValidHttpClient();
            IRateService rateService = new NbpRateService(client);

            // Act
            Rate rate = await rateService.GetAsync("PLN");

            // Assert
            Assert.Null(rate);
        }
    }

    public class SalaryCalculatorServiceTests
    {
        [Fact]
        public async Task CalculateAsync_AmountPLN_ShouldReturnsTheSameAmountAsync()
        {
            // Arrange
            IRateService rateService = new FakePLNRateService();
            SalaryCalculatorService salaryCalculatorService = new SalaryCalculatorService(rateService);

            // Act
            decimal result = await salaryCalculatorService.CalculateAsync(100, "PLN");

            // Assert
            Assert.Equal(expected: 100, result);
        }

        [Fact]
        public async Task CalculateAsync_AmountEUR_ShouldReturnsAmountByEURRatioAsync()
        {
            // Arrange
            IRateService rateService = new FakeEURRateServiceValid();
            SalaryCalculatorService salaryCalculatorService = new SalaryCalculatorService(rateService);

            // Act
            decimal result = await salaryCalculatorService.CalculateAsync(100, "EUR");

            // Assert
            Assert.Equal(expected: 401, result);

        }

        [Fact]
        public async Task CalculateAsync_AmountUSD_ShouldReturnsAmountByUSDRatioAsync()
        {
            // Arrange            
            IRateService rateService = new FakeUSDRateServiceValid();

            SalaryCalculatorService salaryCalculatorService = new SalaryCalculatorService(rateService);

            // Act
            decimal result = await salaryCalculatorService.CalculateAsync(100, "USD");

            // Assert
            Assert.Equal(expected: 400, result);
        }
    }
}
