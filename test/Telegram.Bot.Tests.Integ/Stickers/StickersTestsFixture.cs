using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Stickers
{
    public class StickersTestsFixture
    {
        public StickerSet StickerSet { get; set; }

        public File UploadedSticker { get; set; }

        //public bool ShouldCreateStickerPack { get; set; } = false;

        public string StickerPackName { get; set; }

        public TestsFixture TestsFixture { get; }

        public StickersTestsFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;
        }
    }
}
