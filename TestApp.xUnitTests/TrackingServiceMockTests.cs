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
    public class TrackingServiceLinqToMockTests
    {
        private Lazy<TrackingService> lazyTrackingService;

        private IFileReader fileReader;

        private TrackingService trackingService => lazyTrackingService.Value;

        public TrackingServiceLinqToMockTests()
        {
            lazyTrackingService = new Lazy<TrackingService>(() => new TrackingService(fileReader));
        }

        [Fact]
        public void Get_EmptyFile_ShouldThrowApplicationException()
        {
            // Arange
            fileReader = Mock.Of<IFileReader>(fr => fr.ReadAllText(It.IsAny<string>()) == string.Empty);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<ApplicationException>(act);
        }

        [Fact]
        public void Get_InvalidFile_ShouldThrowFormatException()
        {
            // Arange
            fileReader = Mock.Of<IFileReader>(fr => fr.ReadAllText(It.IsAny<string>()) == "a");

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void Get_ValidFile_ShouldReturnsLocation()
        {
            // Arange
            fileReader = Mock.Of<IFileReader>(fr => fr.ReadAllText(It.IsAny<string>()) == "{\"Latitude\":53.010001,\"Longitude\":18.990001}");

            // Act
            Location result = trackingService.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(53.010001, result.Latitude);
            Assert.Equal(18.990001, result.Longitude);
        }
    }


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
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
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
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
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
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
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
