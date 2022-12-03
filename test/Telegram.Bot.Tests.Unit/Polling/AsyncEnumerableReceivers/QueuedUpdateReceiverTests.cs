using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Polling.AsyncEnumerableReceivers;

public class QueuedUpdateReceiverTests
{
    [Fact]
    public void CallingGetEnumeratorTwiceThrows()
    {
        MockTelegramBotClient mockClient = new();
        QueuedUpdateReceiver receiver = new(mockClient);

        _ = receiver.GetAsyncEnumerator();

        Assert.Throws<InvalidOperationException>(() => receiver.GetAsyncEnumerator());
    }

    [Fact]
    public async Task ReceivesUpdatesInTheBackground()
    {
        MockTelegramBotClient mockClient = new("1", "2", "3");
        QueuedUpdateReceiver receiver = new(mockClient);

        Assert.Equal(3, mockClient.MessageGroupsLeft);

        await foreach (Update update in receiver)
        {
            Assert.Equal("1", update.Message!.Text);
            await Task.Delay(100);
            break;
        }

        Assert.Equal(0, mockClient.MessageGroupsLeft);
        Assert.Equal(2, receiver.PendingUpdates);
    }

    [Fact]
    public async Task ReturnsReceivedPendingUpdates()
    {
        MockTelegramBotClient mockClient = new("foo-bar", "123", "one-two-three", "456");
        QueuedUpdateReceiver receiver = new(mockClient);
        mockClient.Options.RequestDelay = 250;

        Assert.Equal(4, mockClient.MessageGroupsLeft);
        Assert.Equal(0, receiver.PendingUpdates);

        await using IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator();

        Assert.True(await enumerator.MoveNextAsync());

        Assert.Equal(3, mockClient.MessageGroupsLeft);
        Assert.Equal(1, receiver.PendingUpdates);
        Assert.Equal("foo", enumerator.Current.Message!.Text);

        Assert.True(await enumerator.MoveNextAsync());

        Assert.Equal(3, mockClient.MessageGroupsLeft);
        Assert.Equal(0, receiver.PendingUpdates);
        Assert.Equal("bar", enumerator.Current.Message!.Text);

        Assert.True(await enumerator.MoveNextAsync());

        Assert.Equal(2, mockClient.MessageGroupsLeft);
        Assert.Equal(0, receiver.PendingUpdates);
        Assert.Equal("123", enumerator.Current.Message!.Text);

        Assert.True(await enumerator.MoveNextAsync());

        Assert.Equal(1, mockClient.MessageGroupsLeft);
        Assert.Equal(2, receiver.PendingUpdates);
        Assert.Equal("one", enumerator.Current.Message!.Text);

        Assert.True(await enumerator.MoveNextAsync());

        Assert.Equal(1, mockClient.MessageGroupsLeft);
        Assert.Equal(1, receiver.PendingUpdates);
        Assert.Equal("two", enumerator.Current.Message!.Text);

        Assert.True(await enumerator.MoveNextAsync());

        Assert.Equal(1, mockClient.MessageGroupsLeft);
        Assert.Equal(0, receiver.PendingUpdates);
        Assert.Equal("three", enumerator.Current.Message!.Text);

        Assert.True(await enumerator.MoveNextAsync());

        Assert.Equal(0, mockClient.MessageGroupsLeft);
        Assert.Equal(0, receiver.PendingUpdates);
        Assert.Equal("456", enumerator.Current.Message!.Text);
    }

    [Fact]
    public async Task ThrowsOnMoveNextIfCancelled()
    {
        MockTelegramBotClient mockClient = new("foo", "bar");
        QueuedUpdateReceiver receiver = new(mockClient);
        CancellationTokenSource cts = new();

        await using IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator(cts.Token);

        Assert.True(await enumerator.MoveNextAsync());
        Assert.Equal("foo", enumerator.Current.Message?.Text);

        cts.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());
    }

    [Fact]
    public async Task DoesntReceiveAfterCancellation()
    {
        MockTelegramBotClient mockClient = new("foo", "bar", "foo");
        QueuedUpdateReceiver receiver = new(mockClient);

        mockClient.Options.RequestDelay = 200;

        CancellationTokenSource cts = new();

        await using IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator(cts.Token);

        Assert.True(await enumerator.MoveNextAsync());
        Assert.Equal(2, mockClient.MessageGroupsLeft);
        Assert.Equal("foo", enumerator.Current.Message?.Text);

        cts.CancelAfter(50);

        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());

        // ReSharper disable once MethodSupportsCancellation
        await Task.Delay(500);

        Assert.Equal(2, mockClient.MessageGroupsLeft);
    }

    [Fact]
    public void ProvidingNullBotClientThrows()
    {
        Assert.Throws<ArgumentNullException>(() => new QueuedUpdateReceiver(botClient: null!));
    }

    [Fact]
    public async Task MoveNextThrowsIfEnumeratorIsDisposed()
    {
        MockTelegramBotClient mockClient = new("foo");
        QueuedUpdateReceiver receiver = new(mockClient);

        IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator();

        await enumerator.MoveNextAsync();
        await enumerator.DisposeAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());
    }

    [Fact]
    public async Task ExceptionIsCaughtByErrorHandler()
    {
        MockTelegramBotClient mockClient = new() { Options = { ExceptionToThrow = new("Oops") } };
        CancellationTokenSource cts = new();
        bool seenException = false;

        QueuedUpdateReceiver receiver = new(mockClient, pollingErrorHandler: (ex, _) =>
        {
            Assert.Same(mockClient.Options.ExceptionToThrow, ex);
            seenException = true;
            cts.Cancel();
            return Task.CompletedTask;
        });

        await using IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator(cts.Token);

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());
        Assert.True(seenException);
    }

    [Fact]
    public async Task ReceivingIsCanceledOnExceptionIfThereIsNoErrorHandler()
    {
        MockTelegramBotClient mockClient = new() { Options = { ExceptionToThrow = new("Oops") } };
        QueuedUpdateReceiver receiver = new(mockClient);

        await using IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator();

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());

        Exception exception = await Assert.ThrowsAsync<Exception>(async () => await enumerator.MoveNextAsync());
        Assert.Same(mockClient.Options.ExceptionToThrow, exception.InnerException);
    }

    [Fact]
    public async Task UncaughtExceptionIsStoredIfErrorHandlerThrows()
    {
        MockTelegramBotClient mockClient = new() { Options = { ExceptionToThrow = new("Oops") } };
        Exception? exceptionFromErrorHandler = null;

        QueuedUpdateReceiver receiver = new(mockClient, pollingErrorHandler: (ex, _) =>
        {
            Assert.Same(mockClient.Options.ExceptionToThrow, ex);
            throw exceptionFromErrorHandler = new("Oops2");
        });

        await using IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator();

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());

        Exception exception = await Assert.ThrowsAsync<Exception>(async () => await enumerator.MoveNextAsync());
        AggregateException aggregateException = Assert.IsType<AggregateException>(exception.InnerException);

        Assert.NotNull(aggregateException);
        Assert.Equal(2, aggregateException.InnerExceptions.Count);
        Assert.Same(mockClient.Options.ExceptionToThrow, aggregateException.InnerExceptions[0]);
        Assert.Same(exceptionFromErrorHandler, aggregateException.InnerExceptions[1]);
    }
}
