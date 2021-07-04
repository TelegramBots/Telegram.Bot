using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Telegram.Bot.Extensions.Polling.Tests.AsyncEnumerableReceivers
{
    public class QueuedUpdateReceiverTests
    {
        [Fact]
        public void CallingGetEnumeratorTwiceThrows()
        {
            var mockClient = new MockTelegramBotClient();
            var receiver = new QueuedUpdateReceiver(mockClient);

            _ = receiver.GetAsyncEnumerator();

            Assert.Throws<InvalidOperationException>(() => receiver.GetAsyncEnumerator());
        }

        [Fact]
        public async Task ReceivesUpdatesInTheBackground()
        {
            var mockClient = new MockTelegramBotClient("1", "2", "3");
            var receiver = new QueuedUpdateReceiver(mockClient);

            Assert.Equal(3, mockClient.MessageGroupsLeft);

            await foreach (var update in receiver)
            {
                Assert.Equal("1", update.Message.Text);
                await Task.Delay(100);
                break;
            }

            Assert.Equal(0, mockClient.MessageGroupsLeft);
            Assert.Equal(2, receiver.PendingUpdates);
        }

        [Fact]
        public async Task ReturnsReceivedPendingUpdates()
        {
            var mockClient = new MockTelegramBotClient("foo-bar", "123", "one-two-three", "456");
            var receiver = new QueuedUpdateReceiver(mockClient);

            mockClient.Options.RequestDelay = 250;

            Assert.Equal(4, mockClient.MessageGroupsLeft);
            Assert.Equal(0, receiver.PendingUpdates);

            await using var enumerator = receiver.GetAsyncEnumerator();

            Assert.True(await enumerator.MoveNextAsync());

            Assert.Equal(3, mockClient.MessageGroupsLeft);
            Assert.Equal(1, receiver.PendingUpdates);
            Assert.Equal("foo", enumerator.Current.Message.Text);

            Assert.True(await enumerator.MoveNextAsync());

            Assert.Equal(3, mockClient.MessageGroupsLeft);
            Assert.Equal(0, receiver.PendingUpdates);
            Assert.Equal("bar", enumerator.Current.Message.Text);

            Assert.True(await enumerator.MoveNextAsync());

            Assert.Equal(2, mockClient.MessageGroupsLeft);
            Assert.Equal(0, receiver.PendingUpdates);
            Assert.Equal("123", enumerator.Current.Message.Text);

            Assert.True(await enumerator.MoveNextAsync());

            Assert.Equal(1, mockClient.MessageGroupsLeft);
            Assert.Equal(2, receiver.PendingUpdates);
            Assert.Equal("one", enumerator.Current.Message.Text);

            Assert.True(await enumerator.MoveNextAsync());

            Assert.Equal(1, mockClient.MessageGroupsLeft);
            Assert.Equal(1, receiver.PendingUpdates);
            Assert.Equal("two", enumerator.Current.Message.Text);

            Assert.True(await enumerator.MoveNextAsync());

            Assert.Equal(1, mockClient.MessageGroupsLeft);
            Assert.Equal(0, receiver.PendingUpdates);
            Assert.Equal("three", enumerator.Current.Message.Text);

            Assert.True(await enumerator.MoveNextAsync());

            Assert.Equal(0, mockClient.MessageGroupsLeft);
            Assert.Equal(0, receiver.PendingUpdates);
            Assert.Equal("456", enumerator.Current.Message.Text);
        }

        [Fact]
        public async Task ThrowsOnMoveNextIfCancelled()
        {
            var mockClient = new MockTelegramBotClient("foo", "bar");
            var receiver = new QueuedUpdateReceiver(mockClient);

            var cts = new CancellationTokenSource();

            await using var enumerator = receiver.GetAsyncEnumerator(cts.Token);

            Assert.True(await enumerator.MoveNextAsync());
            Assert.Equal("foo", enumerator.Current.Message.Text);

            cts.Cancel();
            await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());
        }

        [Fact]
        public async Task DoesntReceiveAfterCancellation()
        {
            var mockClient = new MockTelegramBotClient("foo", "bar", "foo");
            var receiver = new QueuedUpdateReceiver(mockClient);

            mockClient.Options.RequestDelay = 200;

            var cts = new CancellationTokenSource();

            await using var enumerator = receiver.GetAsyncEnumerator(cts.Token);

            Assert.True(await enumerator.MoveNextAsync());
            Assert.Equal(2, mockClient.MessageGroupsLeft);
            Assert.Equal("foo", enumerator.Current.Message.Text);

            cts.CancelAfter(50);

            await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());

            await Task.Delay(500);

            Assert.Equal(2, mockClient.MessageGroupsLeft);
        }

        [Fact]
        public void ProvidingNullBotClientThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new QueuedUpdateReceiver(botClient: null));
        }

        [Fact]
        public async Task MoveNextThrowsIfEnumeratorIsDisposed()
        {
            var mockClient = new MockTelegramBotClient("foo");
            var receiver = new QueuedUpdateReceiver(mockClient);

            var enumerator = receiver.GetAsyncEnumerator();

            await enumerator.MoveNextAsync();
            await enumerator.DisposeAsync();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());
        }

        [Fact]
        public async Task ExceptionIsCaughtByErrorHandler()
        {
            var mockClient = new MockTelegramBotClient();
            mockClient.Options.ExceptionToThrow = new Exception("Oops");

            var cts = new CancellationTokenSource();

            bool seenException = false;
            var receiver = new QueuedUpdateReceiver(mockClient, errorHandler: (ex, ct) =>
            {
                Assert.Same(mockClient.Options.ExceptionToThrow, ex);
                seenException = true;
                cts.Cancel();
                return Task.CompletedTask;
            });

            await using var enumerator = receiver.GetAsyncEnumerator(cts.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());

            Assert.True(seenException);
        }

        [Fact]
        public async Task ReceivingIsCanceledOnExceptionIfThereIsNoErrorHandler()
        {
            var mockClient = new MockTelegramBotClient();
            mockClient.Options.ExceptionToThrow = new Exception("Oops");

            var receiver = new QueuedUpdateReceiver(mockClient);

            await using var enumerator = receiver.GetAsyncEnumerator();

            await Assert.ThrowsAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await enumerator.MoveNextAsync());
            Assert.Same(mockClient.Options.ExceptionToThrow, ex.InnerException);
        }

        [Fact]
        public async Task UncaughtExceptionIsStoredIfErrorHandlerThrows()
        {
            var mockClient = new MockTelegramBotClient();
            mockClient.Options.ExceptionToThrow = new Exception("Oops");

            Exception exceptionFromErrorHandler = null;

            var receiver = new QueuedUpdateReceiver(mockClient, errorHandler: (ex, ct) =>
            {
                Assert.Same(mockClient.Options.ExceptionToThrow, ex);
                throw (exceptionFromErrorHandler = new Exception("Oops2"));
            });

            await using var enumerator = receiver.GetAsyncEnumerator();

            await Assert.ThrowsAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await enumerator.MoveNextAsync());

            var aggregateEx = ex.InnerException as AggregateException;
            Assert.NotNull(aggregateEx);
            Assert.Equal(2, aggregateEx.InnerExceptions.Count);
            Assert.Same(mockClient.Options.ExceptionToThrow, aggregateEx.InnerExceptions[0]);
            Assert.Same(exceptionFromErrorHandler, aggregateEx.InnerExceptions[1]);
        }
    }
}
