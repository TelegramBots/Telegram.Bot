using System.Collections.Generic;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Stickers
{
    public class StickersTestsFixture
    {
        public StickerSet StickerSet { get; set; }

        public IEnumerable<File> UploadedStickers { get; set; }

        public string StickerSetName { get; set; }

        public int OwnerUserId { get; }

        public TestsFixture TestsFixture { get; }

        public StickersTestsFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;

            int? ownerUserId = ConfigurationProvider.TestConfigurations.StickerOwnerUserId;
            if (ownerUserId == default)
            {
                // ToDo: use /me command to select owner at test execution time
                ownerUserId = 0;
            }

            OwnerUserId = ownerUserId.Value;
        }
    }
}
