using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.AdminBots
{
    public class AdminBotTestFixture
    {
        public TestsFixture TestsFixture { get; }

        public ChatId ChatId { get; set; }

        public string ChatTitle { get; set; } = "Test Chat Title";

        public Message PinnedMessage { get; set; }

        public AdminBotTestFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;
        }
    }
}
