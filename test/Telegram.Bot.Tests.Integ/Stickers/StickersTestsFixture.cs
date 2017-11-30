using System;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Stickers
{
    public class StickersTestsFixture
    {
        public StickerSet StickerSet { get; set; }

        public File UploadedSticker { get; set; }

        public bool ShouldCreateStickerPack { get; set; } = false;

        public User BotUser { get; }

        public string StickerPackName => string.Format(Constants.StickerPackName, BotUser.Username);

        public readonly string StickerPackEmoji = Constants.SmilingFaceEmoji;

        public TestsFixture TestsFixture { get; }

        public StickersTestsFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;

            BotUser = TestsFixture.BotClient.GetMeAsync().Result;

            try
            {
                StickerSet stickerSet = TestsFixture.BotClient.GetStickerSetAsync(StickerPackName).Result;
                ShouldCreateStickerPack = stickerSet != default(StickerSet);
            }
            catch (Exception e){ throw e; }
            }

        public static class Constants
        {
            public const string StickerPackName = "test_by_{0}";

            public const string SmilingFaceEmoji = "\u263A"; // ☺
        }
    }
}
