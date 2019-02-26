using System.Threading.Tasks;
using Xunit;

namespace Telegram.Bot.Extensions.Polling.Tests.YieldingUpdateReceivers
{
    public class QueuedUpdateReceiverTests
    {
        [Fact]
        public async Task ReceivesUpdatesInTheBackground()
        {
            var mockClient = new MockTelegramBotClient("1", "2", "3");
            var receiver = new QueuedUpdateReceiver(mockClient);

            Assert.Equal(3, mockClient.MessageGroupsLeft);

            receiver.StartReceiving();

            await foreach (var update in receiver.YieldUpdatesAsync())
            {
                Assert.Equal("1", update.Message.Text);
                await Task.Delay(100);
                break;
            }

            Assert.Equal(0, mockClient.MessageGroupsLeft);
            Assert.Equal(2, receiver.PendingUpdates);

            receiver.StopReceiving();
        }

        [Fact]
        public async Task StopsIfStoppedAndOutOfUpdates()
        {
            var mockClient = new MockTelegramBotClient("1", "2", "3");
            var receiver = new QueuedUpdateReceiver(mockClient);

            receiver.StartReceiving();

            Task stopTask = Task.Run(() =>
            {
                Task.Delay(150).Wait();
                receiver.StopReceiving();
            });

            int updateCount = 0;
            await foreach (var update in receiver.YieldUpdatesAsync())
            {
                updateCount++;
            }

            Assert.Equal(3, updateCount);
            Assert.Equal(0, mockClient.MessageGroupsLeft);
            Assert.Equal(0, receiver.PendingUpdates);
            Assert.False(receiver.IsReceiving);

            Assert.True(stopTask.IsCompleted);
        }

        [Fact]
        public async Task ReturnsReceivedPendingUpdates()
        {
            var mockClient = new MockTelegramBotClient("foo-bar", "123");
            var receiver = new QueuedUpdateReceiver(mockClient);

            Assert.Equal(2, mockClient.MessageGroupsLeft);
            Assert.Equal(0, receiver.PendingUpdates);

            receiver.StartReceiving();
            await Task.Delay(200);

            Assert.Equal(0, mockClient.MessageGroupsLeft);
            Assert.Equal(3, receiver.PendingUpdates);

            await foreach (var update in receiver.YieldUpdatesAsync())
            {
                Assert.Equal("foo", update.Message.Text);
                break;
            }

            Assert.Equal(2, receiver.PendingUpdates);

            await foreach (var update in receiver.YieldUpdatesAsync())
            {
                Assert.Equal("bar", update.Message.Text);
                break;
            }

            Assert.Equal(1, receiver.PendingUpdates);

            await foreach (var update in receiver.YieldUpdatesAsync())
            {
                Assert.Equal("123", update.Message.Text);
                break;
            }

            Assert.Equal(0, receiver.PendingUpdates);

            receiver.StopReceiving();

            await foreach (var update in receiver.YieldUpdatesAsync())
            {
                // No pending updates and we've called StopReceiving
                Assert.False(true);
            }
        }
    }
}
