using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Stickers
{
    [Collection(CommonConstants.TestCollections.Stickers)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class StickersTests : IClassFixture<StickersTestsFixture>
    {
        private readonly StickersTestsFixture _classFixture;

        private readonly TestsFixture _fixture;

        private ITelegramBotClient BotClient => _fixture.BotClient;

        public StickersTests(StickersTestsFixture classFixture)
        {
            _classFixture = classFixture;
            _fixture = _classFixture.TestsFixture;
        }

        #region 1. Get and send stickers from a set

        [Fact(DisplayName = FactTitles.ShouldGetStickerSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.GetStickerSet)]
        [ExecutionOrder(1.1)]
        public async Task Should_Get_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetStickerSet);
            const string setName = "EvilMinds";
            StickerSet stickerSet = await BotClient.GetStickerSetAsync(setName);

            Assert.Equal(setName, stickerSet.Name);
            Assert.Equal("Evil Minds", stickerSet.Title);
            Assert.False(stickerSet.ContainsMasks);
            Assert.NotEmpty(stickerSet.Stickers);
            Assert.True(20 < stickerSet.Stickers.Count);
            // ToDo: Add more asserts

            _classFixture.StickerSet = stickerSet;
        }

        [Fact(DisplayName = FactTitles.ShouldSendSticker)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendSticker)]
        [ExecutionOrder(1.2)]
        public async Task Should_Send_Sticker()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendSticker);
            FileToSend stickerFile = new FileToSend(_classFixture.StickerSet.Stickers[0].FileId);

            Message message = await _fixture.BotClient.SendStickerAsync(_fixture.SuperGroupChatId, stickerFile);

            Assert.Equal(MessageType.StickerMessage, message.Type);
            Assert.Equal(stickerFile.FileId, message.Sticker.FileId);
        }
        #endregion

        #region 2. Create sticker set

        [Fact(DisplayName = FactTitles.ShouldUploadStickerFile)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.UploadStickerFile)]
        [ExecutionOrder(2.1)]
        public async Task Should_Upload_Stickers()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUploadStickerFile);

            File file;
            using (var stream = System.IO.File.OpenRead("Files/photo/gnu.png"))
            {
                file = await BotClient.UploadStickerFileAsync(
                    _fixture.BotUser.Id,
                    stream.ToFileToSend("GNU")
                );
            }

            Assert.NotEmpty(file.FileId);
            Assert.True(file.FileSize > 0);

            _classFixture.UploadedSticker = file;
        }

        // ToDo: Create sticker with file sent
        // ToDo: add more stickers to set
        // ToDo: Create sticker with mask positions
        // ToDo: Keep file_id of all sent stickers and delete them from sticker set at the end of tests

        #endregion

        private static class FactTitles
        {
            public const string ShouldGetStickerSet = "Should get sticker set";

            public const string ShouldSendSticker = "Should send sticker";

            public const string ShouldUploadStickerFile = "Should upload a sticker file to get file_id";
        }
    }
}
