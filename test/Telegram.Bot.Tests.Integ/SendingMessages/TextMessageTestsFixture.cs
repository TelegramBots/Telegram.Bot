using Telegram.Bot.Tests.Integ.Common;

namespace Telegram.Bot.Tests.Integ.SendingMessages
{
    public class TextMessageTestsFixture : ChannelChatFixture
    {
        public TextMessageTestsFixture(TestsFixture testsFixture)
            : base(testsFixture, Constants.TestCollections.TextMessage)
        { }
    }
}
