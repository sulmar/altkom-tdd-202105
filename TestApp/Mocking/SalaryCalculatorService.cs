using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Mocking
{

    public interface IHttpClient
    {
        Task<RatesList[]> GetFromJsonAsync<T>(string url);
    }

   

    public class StandardHttpClient : IHttpClient
    {
        public async Task<RatesList[]> GetFromJsonAsync<T>(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.nbp.pl/");

            var rates = await client.GetFromJsonAsync<RatesList[]>(url);

            return rates;
        }

        
    }

    public interface IRateService
    {
        Task<Rate> GetAsync(string currencyCode);
    }

    public class NbpRateService : IRateService
    {
        const string url = "api/exchangerates/tables/a/?format=json";

        private readonly IHttpClient client;

        public NbpRateService(IHttpClient client)
        {
            this.client = client;
        }

        public async Task<Rate> GetAsync(string currencyCode)
        {
            return await GetRate(currencyCode);
        }

        private async Task<Rate> GetRate(string currencyCode)
        {
            RatesList[] rates = await GetRates();

            Rate rate = rates.SelectMany(p => p.rates).SingleOrDefault(r => r.code == currencyCode);

            return rate;
        }

        private async Task<RatesList[]> GetRates()
        {
            return await client.GetFromJsonAsync<RatesList[]>(url);
        }
    }

    public class SalaryCalculatorService
    {
        private readonly IRateService rateService;

        public SalaryCalculatorService(IRateService rateService)
        {
            this.rateService = rateService;
        }

        public async Task<decimal> CalculateAsync(decimal amount, string currencyCode = "PLN")
        {
            if (currencyCode == "PLN")
                return amount;

            Rate rate = await rateService.GetAsync(currencyCode);

            //if (rate == null)
            //    rate = new Rate { code = "PLN", mid = 1 };

            decimal result = amount * (decimal)rate.mid;

            return result;

        }

        
    }


    public class RatesList
    {
        public string table { get; set; }
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public Rate[] rates { get; set; }
    }

    public class Rate
    {
        public string currency { get; set; }
        public string code { get; set; }
        public float mid { get; set; }
    }
}
