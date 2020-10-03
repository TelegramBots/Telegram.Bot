using System.Threading;
using Xunit;

namespace Telegram.Bot.Tests.Unit
{
    public class StartReceivingTests
    {
        [Fact]
        public void Should_Not_ReceiveUpdates_On_CancelledToken()
        {
            // Arrange
            var botClient = new TelegramBotClient("1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy");
            var tokenSource = new CancellationTokenSource();

            tokenSource.Cancel();

            // Act
            botClient.StartReceiving(cancellationToken: tokenSource.Token);

            // Assert
            Assert.False(botClient.IsReceiving);
        }
    }
}
