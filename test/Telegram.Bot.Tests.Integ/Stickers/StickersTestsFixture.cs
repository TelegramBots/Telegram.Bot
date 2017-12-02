using System.Collections.Generic;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Stickers
{
    public class StickersTestsFixture
    {
        public StickerSet StickerSet { get; set; }

        public List<File> UploadedStickers { get; set; } = new List<File>();

        public string StickerPackName { get; set; }

        public int UserId { get; }

        public TestsFixture TestsFixture { get; }

        public StickersTestsFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;

            UserId = (int)ConfigurationProvider.TestConfigurations.TesterPrivateChatId;
        }
    }
}
