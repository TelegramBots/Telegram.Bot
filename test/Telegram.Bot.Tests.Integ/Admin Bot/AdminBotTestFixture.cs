using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Admin_Bot
{
    public class AdminBotTestFixture
    {
        public Chat Chat { get; set; }

        public string ChatTitle { get; set; } = "Test Chat Title";

        public Message PinnedMessage { get; set; }
    }
}
