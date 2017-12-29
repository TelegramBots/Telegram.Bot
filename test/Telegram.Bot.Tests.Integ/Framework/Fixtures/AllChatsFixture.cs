using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Framework.Fixtures
{
    public abstract class AllChatsFixture : ChannelChatFixture
    {
        public Chat SupergroupChat { get; }

        public Chat PrivateChat { get; }

        protected AllChatsFixture(TestsFixture testsFixture, string collectionName)
            : base(testsFixture, collectionName)
        {
            var _ = new PrivateChatFixture(testsFixture, collectionName);

            SupergroupChat = testsFixture.SupergroupChat;
            PrivateChat = testsFixture.PrivateChat;
        }
    }
}
