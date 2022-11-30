using Microsoft.Extensions.Logging;

namespace GClient.Test.Extensions
{
    public static class LogLevelExtension
    {
        public static void Verify<T>(this LogLevel logLevel, Times times, Mock<ILogger<T>> logger)
        {
            logger.Verify(x => x.Log(
                   logLevel,
                   It.IsAny<EventId>(),
                   It.IsAny<It.IsAnyType>(),
                   It.IsAny<Exception>(),
                   It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                times);
        }
    }
}
