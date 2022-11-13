using System;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Polling;

public class TestMockClient
{
    [Fact]
    public async Task WorksAsync()
    {
        MockTelegramBotClient bot = new("hello-world", "foo-bar-123");
        Assert.Equal(2, bot.MessageGroupsLeft);

        Update[] updates = await bot.MakeRequestAsync(new GetUpdatesRequest());
        Assert.Equal(2, updates.Length);
        Assert.Equal(1, bot.MessageGroupsLeft);
        Assert.Equal("hello", updates[0].Message!.Text);
        Assert.Equal("world", updates[1].Message!.Text);

        updates = await bot.MakeRequestAsync(new GetUpdatesRequest());
        Assert.Equal(3, updates.Length);
        Assert.Equal(0, bot.MessageGroupsLeft);
        Assert.Equal("foo", updates[0].Message!.Text);
        Assert.Equal("bar", updates[1].Message!.Text);
        Assert.Equal("123", updates[2].Message!.Text);

        updates = await bot.MakeRequestAsync(new GetUpdatesRequest());
        Assert.Empty(updates);
        Assert.Equal(0, bot.MessageGroupsLeft);
    }

    [Fact]
    public async Task ThrowsExceptionIfExceptionToThrownIsSet()
    {
        MockTelegramBotClient bot = new("foo") { Options = { ExceptionToThrow = new("Oops") } };

        Exception ex = await Assert.ThrowsAsync<Exception>(
            async () => await bot.MakeRequestAsync(new GetUpdatesRequest())
        );
        Assert.Equal("Oops", ex.Message);
    }
}
