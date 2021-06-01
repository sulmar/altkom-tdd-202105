using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.IntegrationTests
{
    public class VehiclesControllerFixtureTests : IClassFixture<WebApplicationFactoryWithMock>
    {

        private WebApplicationFactory<WebApi.Startup> factory;

        public VehiclesControllerFixtureTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Get_ValidId_ShouldReturnsOk()
        {
            // Arrange
            var client = factory.CreateClient();

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
            // Arrange 
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("api/vehicles/0");

            // Assets
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(expected: HttpStatusCode.NotFound, response.StatusCode);

        }


    }
}
