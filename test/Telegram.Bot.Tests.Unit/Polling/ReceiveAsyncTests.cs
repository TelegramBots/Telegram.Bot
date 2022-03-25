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
        var bot = new MockTelegramBotClient("start-end", "foo");

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        int updateCount = 0;
        async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            updateCount++;
            Assert.Contains(update.Message!.Text, "start end");
            await Task.Delay(10, cancellationTokenSource.Token);
            if (update.Message.Text == "end")
            {
                cancellationTokenSource.Cancel();
            }
        }

        var updateHandler = new DefaultUpdateHandler(
            updateHandler: HandleUpdate,
            errorHandler: async (_, _, token) => await Task.Delay(10, token)
        );

        var cancellationToken = cancellationTokenSource.Token;
        await bot.ReceiveAsync(updateHandler, cancellationToken: cancellationToken);

        Assert.True(cancellationToken.IsCancellationRequested);
        Assert.Equal(2, updateCount);
        Assert.Equal(1, bot.MessageGroupsLeft);
    }

    [Fact]
    public async Task UserExceptionsPropagateToSurface()
    {
        var bot = new MockTelegramBotClient("foo-bar", "throw");

        int updateCount = 0;
        async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            updateCount++;
            await Task.Delay(10, cancellationToken);
            if (update.Message!.Text == "throw")
                throw new InvalidOperationException("Oops");
        }

        var updateHandler = new DefaultUpdateHandler(
            updateHandler: HandleUpdate,
            errorHandler: async (_, _, token) => await Task.Delay(10, token)
        );

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

    [Fact]
    public async Task ThrowOutPendingUpdates()
    {
        var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(4));

        var bot = new MockTelegramBotClient(
            new MockClientOptions
            {
                Messages = new [] {"foo-bar", "baz", "quux"},
                HandleNegativeOffset = true
            }
        );

        int handleCount = 0;

        Task HandleUpdate(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            handleCount += 1;
            return Task.CompletedTask;
        };

        var updateHandler = new DefaultUpdateHandler(
            updateHandler: HandleUpdate,
            errorHandler: (_, _, _) => Task.CompletedTask
        );

        await bot.ReceiveAsync(
            updateHandler,
            new() { ThrowPendingUpdates = true },
            cancellationTokenSource.Token
        );

        Assert.Equal(0, handleCount);
        Assert.Equal(0, bot.MessageGroupsLeft);
    }
}
