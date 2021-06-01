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
    public class TrackingServiceMockTests
    {
        private Mock<IFileReader> mockFileReader;
        private TrackingService trackingService;

        public TrackingServiceMockTests()
        {
            // Arrange
            mockFileReader = new Mock<IFileReader>();
            trackingService = new TrackingService(mockFileReader.Object);
        }

        [Fact]
        public void Get_EmptyFile_ShouldThrowApplicationException()
        {
            // Arrange
            mockFileReader
                .Setup(fr => fr.ReadAllText("tracking.txt"))
                .Returns(string.Empty);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<ApplicationException>(act);
        }

        [Fact]
        public void Get_InvalidFile_ShouldThrowFormatException()
        {
            // Arrange
            mockFileReader
                .Setup(fr => fr.ReadAllText("tracking.txt"))
                .Returns("a");

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<FormatException>(act);
        }


        // dotnet add package Moq

        // Mock - atrapa

        [Fact]
        public void Get_ValidFile_ShouldReturnsLocation()
        {
            // Arrange
            mockFileReader
                .Setup(fr => fr.ReadAllText("tracking.txt"))
                .Returns("{\"Latitude\":53.010001,\"Longitude\":18.990001}");

            // Act
            Location result = trackingService.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(53.010001, result.Latitude);
            Assert.Equal(18.990001, result.Longitude);
        }
    }
}
