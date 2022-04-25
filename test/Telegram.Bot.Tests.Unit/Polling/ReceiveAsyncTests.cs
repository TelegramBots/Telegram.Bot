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
        MockTelegramBotClient bot = new MockTelegramBotClient("start-end", "foo");

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

        DefaultUpdateHandler updateHandler = new DefaultUpdateHandler(updateHandler: HandleUpdate,
            pollingErrorHandler: async (_, _, token) => await Task.Delay(10, token));

        CancellationToken cancellationToken = cancellationTokenSource.Token;
        await bot.ReceiveAsync(updateHandler, cancellationToken: cancellationToken);

        Assert.True(cancellationToken.IsCancellationRequested);
        Assert.Equal(2, updateCount);
        Assert.Equal(1, bot.MessageGroupsLeft);
    }

    [Fact]
    public async Task UserExceptionsPropagateToSurface()
    {
        MockTelegramBotClient bot = new MockTelegramBotClient("foo-bar", "throw");

        int updateCount = 0;
        async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            updateCount++;
            await Task.Delay(10, cancellationToken);
            if (update.Message!.Text == "throw")
                throw new InvalidOperationException("Oops");
        }

        DefaultUpdateHandler updateHandler = new DefaultUpdateHandler(updateHandler: HandleUpdate,
            pollingErrorHandler: async (_, _, token) => await Task.Delay(10, token));

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
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(4));

        MockTelegramBotClient bot = new MockTelegramBotClient(new MockClientOptions
        {
            Messages = new[] { "foo-bar", "baz", "quux" }, HandleNegativeOffset = true
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

        DefaultUpdateHandler updateHandler = new DefaultUpdateHandler(updateHandler: HandleUpdate,
            pollingErrorHandler: (_, _, _) => Task.CompletedTask);

        await bot.ReceiveAsync(
            updateHandler,
            new ReceiverOptions { ThrowPendingUpdates = true },
            cancellationTokenSource.Token
        );

        Assert.Equal(0, handleCount);
        Assert.Equal(0, bot.MessageGroupsLeft);
    }
}
