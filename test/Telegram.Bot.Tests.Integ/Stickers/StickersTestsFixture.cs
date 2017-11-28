using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Stickers
{
    public class StickersTestsFixture
    {
        public TestsFixture TestsFixture { get; }

        public ChatId ChatId { get; set; }

        public StickerSet StickerSet { get; set; }

        public StickersTestsFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;
        }
    }
}
