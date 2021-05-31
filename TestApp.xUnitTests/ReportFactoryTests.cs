using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals.Gus;
using Xunit;

namespace TestApp.xUnitTests
{
    public class ReportFactoryTests
    {
        [Theory]
        [InlineData("P")]
        [InlineData("LP")]
        [InlineData("LF")]
        public void Create_TypeIsPOrLPOrLF_ShouldReturnsLegalPersonality(string type)
        {
            // Act
            var result = ReportFactory.Create(type);

            // Assert
            Assert.IsType<LegalPersonality>(result);
            Assert.IsAssignableFrom<Report>(result);
        }

        [Fact]
        public void Create_TypeIsF_ShouldReturnsSoleTraderReport()
        {
            // Act
            var result = ReportFactory.Create("F");

            // Assert
            Assert.IsType<SoleTraderReport>(result);
            Assert.IsAssignableFrom<Report>(result);
        }

        [Fact]
        public void Create_TypeIsNotPOrLPOrLFOrF_ShouldThrowsNotSupportedException()
        {
            // Act
            Action act = ()=> ReportFactory.Create("A");

            // Asssert
            Assert.Throws<NotSupportedException>(act);
        }
    }
}
