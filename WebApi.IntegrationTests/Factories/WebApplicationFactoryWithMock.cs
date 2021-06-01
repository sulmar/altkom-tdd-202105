using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApi.IRepositories;
using WebApi.Models;

namespace WebApi.IntegrationTests
{
    public class WebApplicationFactoryWithMock : BaseWebApplicationFactory<TestStartup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder.ConfigureServices(services =>
            {
                IVehicleRepository vehicleRepository = Mock.Of<IVehicleRepository>(
               vr => vr.Get(1) == new Vehicle { Id = 1, Model = "Mazda", Name = "6" });

                services.AddScoped<IVehicleRepository>(p => vehicleRepository);
            });

    }

    
}
