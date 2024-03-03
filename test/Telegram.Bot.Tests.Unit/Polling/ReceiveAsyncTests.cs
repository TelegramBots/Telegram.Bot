using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Polling;

public class ReceiveAsyncTests
{
    [Fact]
    public async Task ReceivesUpdatesAndRespectsTheCancellationToken()
    {
        MockTelegramBotClient bot = new("start-end", "foo");

        CancellationTokenSource cancellationTokenSource = new();

        int updatesCount = 0;

        async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Interlocked.Add(ref updatesCount, 1);

            Assert.NotNull(update.Message?.Text);
            Assert.Contains(update.Message.Text, "start end");

            if (update.Message.Text.Contains("end"))
            {
                cancellationTokenSource.Cancel();
            }

            await Task.Delay(10, cancellationTokenSource.Token);
        }

        DefaultUpdateHandler updateHandler = new(
            updateHandler: HandleUpdate,
            errorHandler: async (_, _, token) => await Task.Delay(10, token)
        );

        CancellationToken cancellationToken = cancellationTokenSource.Token;

        try
        {
            await bot.ReceiveAsync(updateHandler, cancellationToken: cancellationToken);
        }
        catch (OperationCanceledException) { }

        Assert.True(cancellationToken.IsCancellationRequested);
        Assert.Equal(expected: 2, updatesCount);
        Assert.Equal(expected: 1, bot.MessageGroupsLeft);
    }

    [Fact]
    public async Task UserExceptionsPropagateToSurface()
    {
        MockTelegramBotClient bot = new("foo-bar", "throw");
        Exception handledException = new();

        int updateCount = 0;
        async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            updateCount++;
            await Task.Yield();
            if (update.Message?.Text is "throw")
            {
                throw new InvalidOperationException("Update handler exception");
            }
        }

        DefaultUpdateHandler updateHandler = new(
            updateHandler: HandleUpdate,
            errorHandler: (_, exception, token) =>
                {
                    handledException = exception;

                    throw new InvalidOperationException("Error handler exception");
                }
        );

        try
        {
            CancellationTokenSource cts = new(millisecondsDelay: 1_000);
            await bot.ReceiveAsync(updateHandler, cancellationToken: cts.Token);
            Assert.True(false);
        }
        catch (AggregateException ex)
        {
            Assert.IsType<InvalidOperationException>(ex.InnerException);
            Assert.Contains("Error handler exception", ex.InnerException.Message);
        }

        Assert.IsType<InvalidOperationException>(handledException);
        Assert.Contains("Update handler exception", handledException.Message);

        Assert.Equal(3, updateCount);
        Assert.Equal(0, bot.MessageGroupsLeft);
    }

    [Fact]
    public async Task ThrowOutPendingUpdates()
    {
        MockTelegramBotClient bot = new(new MockClientOptions
        {
            Messages = ["foo-bar", "baz", "quux"],
            HandleNegativeOffset = true
        });

        int handleCount = 0;

        Task HandleUpdate(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            handleCount += 1;
            return Task.CompletedTask;
        };

        DefaultUpdateHandler updateHandler = new(
            updateHandler: HandleUpdate,
            errorHandler: (_, _, _) => Task.CompletedTask
        );

        try
        {
            CancellationTokenSource cancellationTokenSource = new(TimeSpan.FromSeconds(1));
            await bot.ReceiveAsync(
                updateHandler,
                new() { ThrowPendingUpdates = true },
                cancellationTokenSource.Token
            );
        }
        catch (OperationCanceledException) { }

        Assert.Equal(0, handleCount);
        Assert.Equal(0, bot.MessageGroupsLeft);
    }
}
