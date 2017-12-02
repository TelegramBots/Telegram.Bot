using System.Collections.Generic;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Stickers
{
    public class StickersTestsFixture
    {
        public StickerSet StickerSet { get; set; }

        public List<File> UploadedStickers { get; set; } = new List<File>();

        public int ChatId => (int)TesterPrivateChatId.Identifier;

        public ChatId TesterPrivateChatId { get; }

        public TestsFixture TestsFixture { get; }

        public StickersTestsFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;

            var privateChatId = ConfigurationProvider.TestConfigurations.TesterPrivateChatId;
            if (privateChatId is null)
            {
                TestsFixture.SendTestCollectionNotificationAsync(
                        CommonConstants.TestCollections.Stickers,
                        "A tester should send /test command in a private chat to begin")
                    .Wait();

                TestsFixture.UpdateReceiver.DiscardNewUpdatesAsync().Wait();
                TesterPrivateChatId = TestsFixture.GetChatIdFromTesterAsync(ChatType.Private).Result;
            }
            else
            {
                TesterPrivateChatId = privateChatId;

                TestsFixture.SendTestCollectionNotificationAsync(CommonConstants.TestCollections.Stickers,
                    "All messages for this collection will be sent in private chat")
                    .Wait();
            }
        }
    }
}
