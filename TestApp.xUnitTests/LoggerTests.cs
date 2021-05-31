using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.xUnitTests
{
    public class LoggerTests
    {
        [Fact]
        public void Log_ValidMessage_RaiseMessageLoggedEvent()
        {
            // Arrange
            Logger logger = new Logger();

            // Act
            var messageLoggedEvent = Assert.Raises<LogEventArgs>(
                a => logger.MessageLogged += a,
                 a => logger.MessageLogged -= a,
                 ()=>logger.Log("a"));

            // Assert
            Assert.NotNull(messageLoggedEvent);



        }
    }
}
