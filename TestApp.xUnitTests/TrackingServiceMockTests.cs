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
        [Fact]
        public void Get_EmptyFile_ShouldThrowApplicationException()
        {
            // Arrange
            Mock<IFileReader> mockFileReader = new Mock<IFileReader>();

            mockFileReader
                .Setup(fr => fr.ReadAllText("tracking.txt"))
                .Returns(string.Empty);

            IFileReader fileReader = mockFileReader.Object;

            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<ApplicationException>(act);
        }

        [Fact]
        public void Get_InvalidFile_ShouldThrowFormatException()
        {
            // Arrange
            Mock<IFileReader> mockFileReader = new Mock<IFileReader>();

            mockFileReader
                .Setup(fr => fr.ReadAllText("tracking.txt"))
                .Returns("a");

            IFileReader fileReader = mockFileReader.Object;

            TrackingService trackingService = new TrackingService(fileReader);

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
            Mock<IFileReader> mockFileReader = new Mock<IFileReader>();

            mockFileReader
                .Setup(fr => fr.ReadAllText("tracking.txt"))
                .Returns("{\"Latitude\":53.010001,\"Longitude\":18.990001}");

            IFileReader fileReader = mockFileReader.Object;

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
