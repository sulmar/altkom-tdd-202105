using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace TestApp.xUnitTests
{
    public class MathCalculatorUnitTests
    {
        private MathCalculator mathCalculator;

        public MathCalculatorUnitTests()
        {
            mathCalculator = new MathCalculator();
        }

        // Method_Scenario_ExpectedBehavior

        [Fact]
        public void Add_WhenCalled_ReturnsTheSumOfArguments()
        {
            // Act
            int result = mathCalculator.Add(1, 2);

            // Assert
            Assert.Equal(expected: 3, result);
        }

        [Fact]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgument()
        {
            // Act
            int result = mathCalculator.Max(a: 2, b: 1);

            // Assert
            Assert.Equal(expected: 2, result);
        }

        [Fact]
        public void Max_SecondArgumentIsGreater_ReturnSecondArgument()
        {
            // Act
            int result = mathCalculator.Max(1, 2);

            // Assert
            Assert.Equal(expected: 2, result);
        }

        [Fact]
        public void Max_ArgumentsAreEqual_ReturnsTheSameArgument()
        {
            // Act
            int result = mathCalculator.Max(1, 1);

            // Assert
            Assert.Equal(expected: 1, result);
        }



        //[DataRow(2, 1, 2, DisplayName = "First argument is greater")]
        //[DataRow(1, 2, 2, DisplayName = "Second argument is greater")]
        //[DataRow(1, 1, 1, DisplayName = "Arguments are equal")]
        //[DataTestMethod]

        [Theory]
        [InlineData(2, 1, 2)]
        [InlineData(1, 2, 2)]
        [InlineData(1, 1, 1)]
        public void Max_ValidArguments_ReturnsValidArgument(int first, int second, int expected)
        {
            // Act
            int result = mathCalculator.Max(first, second);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(MathCalculatorTestData))]
        public void Max_ValidArguments2_ReturnsValidArgument(int first, int second, int expected)
        {
            // Act
            int result = mathCalculator.Max(first, second);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(MathCalculatorTestService.Data), MemberType = typeof(MathCalculatorTestService))]
        public void Max_ValidArguments3_ReturnsValidArgument(int first, int second, int expected)
        {
            // Act
            int result = mathCalculator.Max(first, second);

            // Assert
            Assert.Equal(expected, result);
        }
    }

    public class MathCalculatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2, 1, 2 };
            yield return new object[] { 1, 2, 2 };
            yield return new object[] { 1, 1, 1 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class MathCalculatorTestService
    {
        public static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[] { 2, 1, 2 };
                yield return new object[] { 1, 2, 2 };
                yield return new object[] { 1, 1, 1 };
            }
        }
    }
}
