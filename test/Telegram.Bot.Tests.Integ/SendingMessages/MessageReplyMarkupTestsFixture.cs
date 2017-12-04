using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.SendingMessages
{
    public class MessageReplyMarkupTestsFixture
    {
        public TestsFixture TestsFixture { get; }

        public ChatId TesterPrivateChatId { get; }

        public MessageReplyMarkupTestsFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;

            var privateChatId = ConfigurationProvider.TestConfigurations.TesterPrivateChatId;
            if (privateChatId is null)
            {
                TestsFixture.SendTestCollectionNotificationAsync(
                        Constants.TestCollections.MessageReplyMarkup,
                        "A tester should send /test command in a private chat to begin")
                    .Wait();

                TestsFixture.UpdateReceiver.DiscardNewUpdatesAsync().Wait();
                TesterPrivateChatId = TestsFixture.GetChatIdFromTesterAsync(ChatType.Private).Result;
            }
            else
            {
                TesterPrivateChatId = privateChatId;

                TestsFixture.SendTestCollectionNotificationAsync(Constants.TestCollections.MessageReplyMarkup,
                    "All messages for this collection will be sent in private chat")
                    .Wait();

                TestsFixture.SendTestCollectionNotificationAsync(Constants.TestCollections.MessageReplyMarkup, chatid: TesterPrivateChatId)
                    .Wait();
            }
        }
    }
}
