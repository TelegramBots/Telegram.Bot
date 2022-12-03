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

        int updateCount = 0;
        async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            updateCount++;
            Assert.NotNull(update.Message?.Text);
            Assert.Contains(update.Message.Text, "start end");
            await Task.Delay(10, cancellationTokenSource.Token);
            if (update.Message.Text is "end")
            {
                cancellationTokenSource.Cancel();
            }
        }

        DefaultUpdateHandler updateHandler = new(
            updateHandler: HandleUpdate,
            pollingErrorHandler: async (_, _, token) => await Task.Delay(10, token)
        );

        CancellationToken cancellationToken = cancellationTokenSource.Token;
        await bot.ReceiveAsync(updateHandler, cancellationToken: cancellationToken);

        Assert.True(cancellationToken.IsCancellationRequested);
        Assert.Equal(2, updateCount);
        Assert.Equal(1, bot.MessageGroupsLeft);
    }

    [Fact]
    public async Task UserExceptionsPropagateToSurface()
    {
        MockTelegramBotClient bot = new("foo-bar", "throw");

        int updateCount = 0;
        async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            updateCount++;
            await Task.Delay(10, cancellationToken);
            if (update.Message?.Text is "throw")
            {
                throw new InvalidOperationException("Oops");
            }
        }

        DefaultUpdateHandler updateHandler = new(
            updateHandler: HandleUpdate,
            pollingErrorHandler: async (_, _, token) => await Task.Delay(10, token)
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
        CancellationTokenSource cancellationTokenSource = new(TimeSpan.FromSeconds(4));

        MockTelegramBotClient bot = new(new MockClientOptions
        {
            Messages = new[] { "foo-bar", "baz", "quux" },
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
            pollingErrorHandler: (_, _, _) => Task.CompletedTask
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
