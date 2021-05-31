using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.xUnitTests
{
    public class MarkDownFormatterTests
    {
        //[Fact]
        //public void FormatAsBold_ValidContent_ShouldReturnsContentEncloseDoubleAsterix()
        //{
        //    // Arrange
        //    MarkdownFormatter markdownFormatter = new MarkdownFormatter();

        //    // Act
        //    string result = markdownFormatter.FormatAsBold("abc");

        //    // Assert
        //    // Assert.Equal(expected: "**abc**", result);

        //    Assert.StartsWith("**", result);
        //    Assert.Contains("abc", result);
        //    Assert.EndsWith("**", result);
        //}

        [Theory]
        [InlineData("a")]
        [InlineData("abc")]
        [InlineData("Lorem ipsum")]
        public void FormatAsBold_ValidContent_ShouldReturnsContentEncloseDoubleAsterix(string content)
        {
            // Arrange
            MarkdownFormatter markdownFormatter = new MarkdownFormatter();

            // Act
            string result = markdownFormatter.FormatAsBold(content);

            // Assert
            Assert.StartsWith("**", result);
            Assert.Contains(content, result);
            Assert.EndsWith("**", result);
        }

        [Fact]
        public void FormatAsBold_EmptyContent_ShouldThrowsFormatException()
        {
            // Arrange
            MarkdownFormatter markdownFormatter = new MarkdownFormatter();

            // Act
            Action act = () => markdownFormatter.FormatAsBold(string.Empty);

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void FormatAsItalic_ValidContent_ShouldReturnsContentEncloseUndescore()
        {
            // Arrange
            MarkdownFormatter markdownFormatter = new MarkdownFormatter();

            // Act
            string result = markdownFormatter.FormatAsItalic("abc");

            // Assert
            Assert.StartsWith("_", result);
            Assert.Contains("abc", result);
            Assert.EndsWith("_", result);
        }

        [Fact]
        public void FormatAsItalic_EmptyContent_ShouldThrowsFormatException()
        {
            // Arrange
            MarkdownFormatter markdownFormatter = new MarkdownFormatter();

            // Act
            Action act = () => markdownFormatter.FormatAsItalic(string.Empty);

            // Assert
            Assert.Throws<FormatException>(act);
        }

    }
}
