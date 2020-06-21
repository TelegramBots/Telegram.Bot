using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Xunit;

namespace Telegram.Bot.Extensions.Polling.Tests
{
    public class TestMockClient
    {
        [Fact]
        public async Task WorksAsync()
        {
            ITelegramBotClient bot = new MockTelegramBotClient(
                new MockClientOptions("hello-world", "foo-bar-123")
            );

            var updates = await bot.MakeRequestAsync(new GetUpdatesRequest());
            Assert.Equal(2, updates.Length);
            Assert.Equal("hello", updates[0].Message.Text);
            Assert.Equal("world", updates[1].Message.Text);

            updates = await bot.MakeRequestAsync(new GetUpdatesRequest());
            Assert.Equal(3, updates.Length);
            Assert.Equal("foo", updates[0].Message.Text);
            Assert.Equal("bar", updates[1].Message.Text);
            Assert.Equal("123", updates[2].Message.Text);

            updates = await bot.MakeRequestAsync(new GetUpdatesRequest());
            Assert.Empty(updates);
        }
    }
}
