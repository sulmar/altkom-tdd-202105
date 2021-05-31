using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.MSUnitTests
{
    [TestClass]
    public class LoggerTests
    {
        private Logger logger;

        [TestInitialize]
        public void Setup()
        {
            logger = new Logger();
        }

        [TestMethod]
        public void Log_ValidMessage_SetLastMessage()
        {
            // Act
            logger.Log("a");

            // Assert
            Assert.AreEqual(expected: logger.LastMessage, "a");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Log_EmptyMessage_ThrowArgumentNullException()
        {
            // Act
            logger.Log(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Log_NullMessage_ThrowArgumentNullException()
        {
            // Act
            logger.Log(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Log_WhiteSpace_ThrowArgumentNullException()
        {
            // Act
            logger.Log(" ");
        }

        // Metod prywatnych nie testujemy!!!
    }
}
