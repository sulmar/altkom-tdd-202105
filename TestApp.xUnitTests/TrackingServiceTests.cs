using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Mocking;
using Xunit;

namespace TestApp.xUnitTests
{
    public class FakeEmptyFile : IFileReader
    {
        public string ReadAllText(string path)
        {
            return string.Empty;
        }
    }

    public class FakeInvalidFile : IFileReader
    {
        public string ReadAllText(string path)
        {
            return "a";
        }
    }

    public class FakeValidFile : IFileReader
    {
        public string ReadAllText(string path)
        {
            return "{\"Latitude\":53.010001,\"Longitude\":18.990001}";
        }
    }


    public class TrackingServiceTests
    {
        // Method_Scenario_ExpectedBehavior

        // Pusty plik
        [Fact]
        public void Get_EmptyFile_ShouldThrowApplicationException()
        {
            // Arrange
            IFileReader fileReader = new FakeEmptyFile();
            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = () => trackingService.Get();

            // Asserts
            Assert.Throws<ApplicationException>(act);

        }

        // Niepoprawna struktura pliku

        [Fact]
        public void Get_InvalidFile_ShouldThrowFormatException()
        {
            // Arrange
            IFileReader fileReader = new FakeInvalidFile();
            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = () => trackingService.Get();

            // Asserts
            Assert.Throws<FormatException>(act);
        }

        // Poprawny plik
        [Fact]
        public void Get_ValidFile_ShouldReturnsLocation()
        {
            // Arrange
            IFileReader fileReader = new FakeValidFile();
            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            Location result = trackingService.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(53.010001, result.Latitude);
            Assert.Equal(18.990001, result.Longitude);
        }
    }
}
