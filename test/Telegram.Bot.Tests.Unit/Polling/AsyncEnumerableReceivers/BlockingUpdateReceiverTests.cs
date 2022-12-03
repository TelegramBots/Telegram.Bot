using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Polling.AsyncEnumerableReceivers;

public class BlockingUpdateReceiverTests
{
    [Fact]
    public void CallingGetEnumeratorTwiceThrows()
    {
        MockTelegramBotClient mockClient = new();
        BlockingUpdateReceiver receiver = new(mockClient);

        _ = receiver.GetAsyncEnumerator();

        Assert.Throws<InvalidOperationException>(() => receiver.GetAsyncEnumerator());
    }

    [Fact]
    public async Task DoesntReceiveWhileProcessing()
    {
        MockTelegramBotClient mockClient = new("foo", "bar");
        BlockingUpdateReceiver receiver = new(mockClient);

        Assert.Equal(2, mockClient.MessageGroupsLeft);

        await foreach (Update update in receiver)
        {
            Assert.Equal("foo", update.Message?.Text);
            await Task.Delay(100);
            Assert.Equal(1, mockClient.MessageGroupsLeft);
            break;
        }

        Assert.Equal(1, mockClient.MessageGroupsLeft);
    }

    [Fact]
    public async Task ReceivesOnlyOnMoveNextAsync()
    {
        MockTelegramBotClient mockClient = new("foo", "bar");
        BlockingUpdateReceiver receiver = new(mockClient);

        Assert.Equal(2, mockClient.MessageGroupsLeft);

        await using IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator();

        Assert.Equal(2, mockClient.MessageGroupsLeft);

        Assert.True(await enumerator.MoveNextAsync());
        Assert.Equal("foo", enumerator.Current.Message?.Text);

        Assert.Equal(1, mockClient.MessageGroupsLeft);

        Assert.True(await enumerator.MoveNextAsync());
        Assert.Equal("bar", enumerator.Current.Message?.Text);

        Assert.Equal(0, mockClient.MessageGroupsLeft);
    }

    [Fact]
    public async Task ThrowsOnMoveNextIfCancelled()
    {
        MockTelegramBotClient mockClient = new("foo", "bar");
        BlockingUpdateReceiver receiver = new(mockClient);
        CancellationTokenSource cts = new();

        await using IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator(cts.Token);

        Assert.True(await enumerator.MoveNextAsync());
        Assert.Equal("foo", enumerator.Current.Message?.Text);

        mockClient.Options.RequestDelay = 1000;
        cts.CancelAfter(200);

        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());
    }

    [Fact]
    public async Task MoveNextThrowsIfEnumeratorIsDisposed()
    {
        MockTelegramBotClient mockClient = new("foo");
        BlockingUpdateReceiver receiver = new(mockClient);

        IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator();
        await enumerator.MoveNextAsync();
        await enumerator.DisposeAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());
    }

    [Fact]
    public async Task ExceptionIsCaughtByErrorHandler()
    {
        MockTelegramBotClient mockClient = new() { Options = { ExceptionToThrow = new("Oops") } };
        Exception? exceptionFromErrorHandler = null;

        BlockingUpdateReceiver receiver = new(mockClient, pollingErrorHandler: (ex, _) =>
        {
            Assert.Same(mockClient.Options.ExceptionToThrow, ex);
            throw exceptionFromErrorHandler = new("Oops2");
        });

        await using IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator();

        Exception ex = await Assert.ThrowsAsync<Exception>(async () => await enumerator.MoveNextAsync());
        Assert.Same(exceptionFromErrorHandler, ex);
    }

    [Fact]
    public async Task ExceptionIsNotCaughtIfThereIsNoErrorHandler()
    {
        MockTelegramBotClient mockClient = new() { Options = { ExceptionToThrow = new("Oops") } };
        BlockingUpdateReceiver receiver = new(mockClient);

        await using IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator();

        Exception ex = await Assert.ThrowsAsync<Exception>(async () => await enumerator.MoveNextAsync());
        Assert.Same(mockClient.Options.ExceptionToThrow, ex);
    }

    [Fact]
    public async Task ThrowOutPendingUpdates()
    {
        CancellationTokenSource cancellationTokenSource = new(TimeSpan.FromSeconds(4));
        MockTelegramBotClient bot = new(new MockClientOptions
        {
            Messages = new[] { "foo-bar", "baz", "quux" },
            HandleNegativeOffset = true,
        });

        BlockingUpdateReceiver receiver = new(bot, new() { ThrowPendingUpdates = true });

        await using IAsyncEnumerator<Update> enumerator = receiver.GetAsyncEnumerator(cancellationTokenSource.Token);

        try
        {
            await enumerator.MoveNextAsync();
        }
        catch (OperationCanceledException)
        {
            // ignored
        }

        Assert.Equal(0, bot.MessageGroupsLeft);
    }
}
