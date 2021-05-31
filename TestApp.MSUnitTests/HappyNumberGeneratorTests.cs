using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals;

namespace TestApp.MSUnitTests
{
    [TestClass]
    public class HappyNumberGeneratorTests
    {
        [TestMethod]
        public void Generate_WhenCalled_ReturnsSevenNumbers()
        {
            // Arrange
            HappyNumberGenerator generator = new HappyNumberGenerator();

            // Act
            IEnumerable<int> result = generator.Generate();


            // Assert
            result.Should().NotBeEmpty()
                .And.HaveCount(7);
        }
    }
}
