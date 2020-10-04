using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Unit.Framework;
using Xunit;

namespace Telegram.Bot.Tests.Unit
{
    public class StartReceivingTests
    {
        [Fact]
        public void Should_Not_ReceiveUpdates_On_CancelledToken()
        {
            // Arrange
            var httpClientCounter = new RequestCounterHttpClientHandler();
            var botClient = new TelegramBotClient("1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy", new HttpClient(httpClientCounter));
            var tokenSource = new CancellationTokenSource();

            tokenSource.Cancel();

            // Act
            botClient.StartReceiving(cancellationToken: tokenSource.Token);

            // Assert
            Assert.False(botClient.IsReceiving);
            Assert.Equal(0, httpClientCounter.RequestCounter);
        }

        [Fact]
        public async Task Should_ReceiveUpdates_On_ActiveToken()
        {
            // Arrange
            var httpClientCounter = new RequestCounterHttpClientHandler();
            var botClient = new TelegramBotClient("1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy", new HttpClient(httpClientCounter));
            var tokenSource = new CancellationTokenSource();

            // Act
            var cancelTokenTask = Task.Delay(500).ContinueWith((t) => tokenSource.Cancel()); // Cancel Token after 500ms
            botClient.StartReceiving(cancellationToken: tokenSource.Token);
            await cancelTokenTask;

            // Assert
            Assert.NotEqual(0, httpClientCounter.RequestCounter);
        }
    }
}
