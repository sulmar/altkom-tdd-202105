using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.IRepositories;
using WebApi.Models;
using Xunit;

namespace WebApi.IntegrationTests
{

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            IVehicleRepository vehicleRepository = Mock.Of<IVehicleRepository>(
                vr => vr.Get(1) == new Vehicle { Id = 1, Model = "Mazda", Name = "6" });

            services.AddScoped<IVehicleRepository>(p=>vehicleRepository);
        }
    }

    // W przypadku tworzenie zasobo¿ernej infrastruktury patrz IClassFixture 
    // https://github.com/sulmar/sulmar-mon-tdd/blob/master/Api.IntegrationTests/VehiclesControllerFixtureTests.cs

    public class VehiclesControllerTests
    {

        // dotnet add package Microsoft.AspNetCore.TestHost

        private TestServer server;
        private HttpClient client;

        public VehiclesControllerTests()
        {
            server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartup>()                
                .UseEnvironment("Development"));

            client = server.CreateClient();
               
        }

        [Fact]
        public async Task Get_ValidId_ShouldReturnsOk()
        {
            // Arrange

            // Act
            var response = await client.GetAsync("api/vehicles/1");

            // string json = await response.Content.ReadAsStringAsync();

            // Assets
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expected: HttpStatusCode.OK, response.StatusCode);

        }


        [Fact]
        public async Task Get_InvalidId_ShouldReturnsNotFound()
        {
            // Act
            var response = await client.GetAsync("api/vehicles/0");

            // Assets
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(expected: HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}
