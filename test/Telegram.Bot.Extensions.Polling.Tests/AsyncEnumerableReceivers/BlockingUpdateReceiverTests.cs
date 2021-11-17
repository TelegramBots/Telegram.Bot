using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Extensions.Polling.Tests.AsyncEnumerableReceivers;

public class BlockingUpdateReceiverTests
{
    [Fact]
    public void CallingGetEnumeratorTwiceThrows()
    {
        var mockClient = new MockTelegramBotClient();
        var receiver = new BlockingUpdateReceiver(mockClient);

        _ = receiver.GetAsyncEnumerator();

        Assert.Throws<InvalidOperationException>(() => receiver.GetAsyncEnumerator());
    }

    [Fact]
    public async Task DoesntReceiveWhileProcessing()
    {
        var mockClient = new MockTelegramBotClient("foo", "bar");
        var receiver = new BlockingUpdateReceiver(mockClient);

        Assert.Equal(2, mockClient.MessageGroupsLeft);

        await foreach (Update update in receiver)
        {
            Assert.Equal("foo", update.Message!.Text);
            await Task.Delay(100);
            Assert.Equal(1, mockClient.MessageGroupsLeft);
            break;
        }

        Assert.Equal(1, mockClient.MessageGroupsLeft);
    }

    [Fact]
    public async Task ReceivesOnlyOnMoveNextAsync()
    {
        var mockClient = new MockTelegramBotClient("foo", "bar");
        var receiver = new BlockingUpdateReceiver(mockClient);

        Assert.Equal(2, mockClient.MessageGroupsLeft);

        await using var enumerator = receiver.GetAsyncEnumerator();

        Assert.Equal(2, mockClient.MessageGroupsLeft);

        Assert.True(await enumerator.MoveNextAsync());
        Assert.Equal("foo", enumerator.Current.Message!.Text);

        Assert.Equal(1, mockClient.MessageGroupsLeft);

        Assert.True(await enumerator.MoveNextAsync());
        Assert.Equal("bar", enumerator.Current.Message!.Text);

        Assert.Equal(0, mockClient.MessageGroupsLeft);
    }

    [Fact]
    public async Task ThrowsOnMoveNextIfCancelled()
    {
        var mockClient = new MockTelegramBotClient("foo", "bar");
        var receiver = new BlockingUpdateReceiver(mockClient);

        var cts = new CancellationTokenSource();

        await using var enumerator = receiver.GetAsyncEnumerator(cts.Token);

        Assert.True(await enumerator.MoveNextAsync());
        Assert.Equal("foo", enumerator.Current.Message!.Text);

        mockClient.Options.RequestDelay = 1000;
        cts.CancelAfter(200);

        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await enumerator.MoveNextAsync());
    }

    [Fact]
    public async Task MoveNextThrowsIfEnumeratorIsDisposed()
    {
        var mockClient = new MockTelegramBotClient("foo");
        var receiver = new BlockingUpdateReceiver(mockClient);

        var enumerator = receiver.GetAsyncEnumerator();
        await enumerator.MoveNextAsync();

        await enumerator.DisposeAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            async () => await enumerator.MoveNextAsync()
        );
    }

    [Fact]
    public async Task ExceptionIsCaughtByErrorHandler()
    {
        var mockClient = new MockTelegramBotClient
        {
            Options =
            {
                ExceptionToThrow = new("Oops")
            }
        };

        Exception exceptionFromErrorHandler = null;

        var receiver = new BlockingUpdateReceiver(mockClient, errorHandler: (ex, ct) =>
        {
            Assert.Same(mockClient.Options.ExceptionToThrow, ex);
            throw exceptionFromErrorHandler = new("Oops2");
        });

        await using var enumerator = receiver.GetAsyncEnumerator();

        Exception ex = await Assert.ThrowsAsync<Exception>(async () => await enumerator.MoveNextAsync());
        Assert.Same(exceptionFromErrorHandler, ex);
    }

    [Fact]
    public async Task ExceptionIsNotCaughtIfThereIsNoErrorHandler()
    {
        var mockClient = new MockTelegramBotClient
        {
            Options =
            {
                ExceptionToThrow = new("Oops")
            }
        };

        var receiver = new BlockingUpdateReceiver(mockClient);

        await using var enumerator = receiver.GetAsyncEnumerator();

        Exception ex = await Assert.ThrowsAsync<Exception>(async () => await enumerator.MoveNextAsync());
        Assert.Same(mockClient.Options.ExceptionToThrow, ex);
    }

    [Fact]
    public async Task ThrowOutPendingUpdates()
    {
        var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(4));

        var bot = new MockTelegramBotClient(
            new MockClientOptions
            {
                Messages = new [] {"foo-bar", "baz", "quux"},
                HandleNegativeOffset = true,
            }
        );

        var receiver = new BlockingUpdateReceiver(bot, new() {ThrowPendingUpdates = true});

        await using var enumerator = receiver.GetAsyncEnumerator(cancellationTokenSource.Token);

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
