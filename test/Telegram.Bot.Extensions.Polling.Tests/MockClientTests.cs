using Xunit;

namespace Telegram.Bot.Extensions.Polling.Tests
{
    public class TestMockClient
    {
        [Fact]
        public void Works()
        {
            ITelegramBotClient bot = new MockTelegramBotClient("hello-world", "foo-bar-123");

            var updates = bot.GetUpdatesAsync().Result;
            Assert.Equal(2, updates.Length);
            Assert.Equal("hello", updates[0].Message.Text);
            Assert.Equal("world", updates[1].Message.Text);

            updates = bot.GetUpdatesAsync().Result;
            Assert.Equal(3, updates.Length);
            Assert.Equal("foo", updates[0].Message.Text);
            Assert.Equal("bar", updates[1].Message.Text);
            Assert.Equal("123", updates[2].Message.Text);

            updates = bot.GetUpdatesAsync().Result;
            Assert.Empty(updates);
        }
    }
}
