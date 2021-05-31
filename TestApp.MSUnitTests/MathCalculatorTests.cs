using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestApp.MSUnitTests
{
    [TestClass]
    public class MathCalculatorTests
    {
        private MathCalculator mathCalculator;

        // Metoda wywo�ana zostanie dla ka�dego testu
        [TestInitialize]
        public void Setup()
        {
            // Arrange
            mathCalculator = new MathCalculator();
        }

        // Method_Scenario_ExpectedBehavior

        [TestMethod]
        public void Add_WhenCalled_ReturnsTheSumOfArguments()
        {
            // Arrange

            // Act
            int result = mathCalculator.Add(1, 2);

            // Assert
            Assert.AreEqual(expected: 3, actual: result);
        }

        // 1. a jest wi�ksze od b  -> a
        // 2. b jest wi�ksze od a  -> b
        // 3. a i b s� r�wne -> a lub b

        [TestMethod]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgument()
        {
            // Arrange

            // Act
            int result = mathCalculator.Max(a: 2, b: 1);

            // Assert
            Assert.AreEqual(expected: 2, result);
        }

        [TestMethod]
        public void Max_SecondArgumentIsGreater_ReturnSecondArgument()
        {
            // Arrange

            // Act
            int result = mathCalculator.Max(1, 2);

            // Assert
            Assert.AreEqual(expected: 2, result);
        }

        [TestMethod]
        public void Max_ArgumentsAreEqual_ReturnsTheSameArgument()
        {
            // Arrange
            MathCalculator mathCalculator = new MathCalculator();

            // Act
            int result = mathCalculator.Max(1, 1);

            // Assert
            Assert.AreEqual(expected: 1, result);
        }

        [DataRow(2, 1, 2, DisplayName = "First argument is greater")]
        [DataRow(1, 2, 2, DisplayName = "Second argument is greater")]
        [DataRow(1, 1, 1, DisplayName = "Arguments are equal")]
        [DataTestMethod]
        public void Max_ValidArguments_ReturnsValidArgument(int first, int second, int expected)
        {
            // Act
            int result = mathCalculator.Max(first, second);

            // Assert
            Assert.AreEqual(expected, result);
        }
      
    }
}
