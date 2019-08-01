using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Admin_Bot
{
    public class ChannelAdminBotTestFixture
    {
        public Chat Chat { get; }
        public Message PinnedMessage { get; set; }

        public ChannelAdminBotTestFixture(TestsFixture fixture)
        {
            Chat = new ChannelChatFixture(fixture, Constants.TestCollections.ChannelAdminBots).ChannelChat;
        }
    }
}
