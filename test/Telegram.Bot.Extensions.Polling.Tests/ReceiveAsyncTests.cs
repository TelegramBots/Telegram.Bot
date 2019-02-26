using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Extensions.Polling.Tests
{
    public class ReceiveAsyncTests
    {
        [Fact]
        public async Task ReceivesUpdatesAndRespectsTheCancellationToken()
        {
            var bot = new MockTelegramBotClient("start-end", "foo");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            int updateCount = 0;
            async Task HandleUpdate(Update update)
            {
                updateCount++;
                Assert.Contains(update.Message.Text, "start end");
                await Task.Delay(10);
                if (update.Message.Text == "end")
                    cancellationTokenSource.Cancel();
            }

            var updateHandler = new DefaultUpdateHandler(HandleUpdate, errorHandler: async e => await Task.Delay(10));

            var cancellationToken = cancellationTokenSource.Token;
            await bot.ReceiveAsync(updateHandler, cancellationToken);

            Assert.True(cancellationToken.IsCancellationRequested);
            Assert.Equal(2, updateCount);
            Assert.Equal(1, bot.MessageGroupsLeft);
        }

        [Fact]
        public async Task UserExceptionsPropagateToSurface()
        {
            var bot = new MockTelegramBotClient("foo-bar", "throw");

            int updateCount = 0;
            async Task HandleUpdate(Update update)
            {
                updateCount++;
                await Task.Delay(10);
                if (update.Message.Text == "throw")
                    throw new InvalidOperationException("Oops");
            }

            var updateHandler = new DefaultUpdateHandler(HandleUpdate, errorHandler: async e => await Task.Delay(10));

            try
            {
                await bot.ReceiveAsync(updateHandler);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<InvalidOperationException>(ex);
                Assert.Contains("Oops", ex.Message);
            }

            Assert.Equal(3, updateCount);
            Assert.Equal(0, bot.MessageGroupsLeft);
        }
    }
}
