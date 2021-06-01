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
    public class VehiclesViewModelTests
    {


        [Fact]
        public async Task SearchAsync_ValidSearchCriteria_ShouldReturnsVehicles()
        {
            // Arrange
            VehicleSearchCriteria vehicleSearchCriteria = new VehicleSearchCriteria { Model = "Fiat" };

            IVehicleRepository vehicleRepository = Mock.Of<IVehicleRepository>(
                vr => vr.GetAsync(vehicleSearchCriteria) == Task.FromResult<ICollection<Vehicle>>(new List<Vehicle>() { new Vehicle { Model = "Fiat" } }));


            VehiclesViewModel vehiclesViewModel = new VehiclesViewModel(vehicleRepository);

            vehiclesViewModel.Criteria = vehicleSearchCriteria;


            // Act
            await vehiclesViewModel.SearchAsync();

            var result = vehiclesViewModel.Vehicles;

            // Assets
            Assert.NotNull(result);
            Assert.True(result.Any());
            Assert.True(result.All(vehicle => vehicle.Model.Contains("Fiat")));

        }


        [Fact]
        public async Task CanSearch_InvalidSearchCriteria_ShouldReturnsFalse()
        {
            // Arrange
            IVehicleRepository vehicleRepository = Mock.Of<IVehicleRepository>();

            VehiclesViewModel vehiclesViewModel = new VehiclesViewModel(vehicleRepository);

            vehiclesViewModel.Criteria = new VehicleSearchCriteria { Model = string.Empty };

            // Act

            bool result = vehiclesViewModel.CanSearch;


            // Assets

            Assert.False(result);

        }

        [Fact]
        public void CanSearch_ValidSearchCriteria_ShouldReturnsTrue()
        {
            // Arrange
            IVehicleRepository vehicleRepository = Mock.Of<IVehicleRepository>();

            VehiclesViewModel vehiclesViewModel = new VehiclesViewModel(vehicleRepository);

            vehiclesViewModel.Criteria = new VehicleSearchCriteria { Model = "a" };

            // Act

            bool result = vehiclesViewModel.CanSearch;


            // Assets

            Assert.True(result);

        }

    }
}
