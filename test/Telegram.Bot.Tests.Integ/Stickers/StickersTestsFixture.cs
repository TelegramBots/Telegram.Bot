using System.Collections.Generic;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Stickers
{
    public class StickersTestsFixture : PrivateChatFixture
    {
        public StickerSet EvilMindsStickerSet { get; set; }

        public IEnumerable<File> UploadedStickers { get; set; }

        public string TestStickerSetName { get; }

        public StickerSet TestStickerSet { get; set; }

        public int OwnerUserId { get; }

        public StickersTestsFixture(TestsFixture testsFixture)
             : base(testsFixture, Constants.TestCollections.Stickers)
        {
            TestStickerSetName = $"test14_by_{testsFixture.BotUser.Username}";

            OwnerUserId = ConfigurationProvider.TestConfigurations.StickerOwnerUserId ?? (int)testsFixture.PrivateChat.Id;
        }
    }
}
