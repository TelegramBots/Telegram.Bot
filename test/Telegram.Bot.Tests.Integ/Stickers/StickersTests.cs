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

        private User BotUser => _fixture.BotUser;

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

        [Fact(DisplayName = FactTitles.ShouldCreateNewStickerSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.2)]
        public async Task Should_Create_New_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldCreateNewStickerSet);
            FileToSend stickerFile = new FileToSend(_classFixture.UploadedSticker.FileId);
            _classFixture.StickerPackName = $"stickers_test_by_{_fixture.BotUser.Username}";

            bool result = await BotClient.CreateNewStickerSetAsnyc(
                 _fixture.BotUser.Id,
                 _classFixture.StickerPackName,
                 _classFixture.StickerPackName,
                 stickerFile,
                 Constants.SmilingFaceEmoji);

            Assert.True(result);
        }

        /*
        [Fact(DisplayName = FactTitles.ShouldCreateNewMasksStickerSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.3)]
        public async Task Should_Create_New_Masks_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldCreateNewMasksStickerSet);
            FileToSend stickerFile = new FileToSend(_classFixture.UploadedSticker.FileId);
            _classFixture.StickerPackName = $"masks_test_by_{_classFixture.BotUser.Username}";
            MaskPosition maskPosition = new MaskPosition()
            {
                Point = MaskPositionPoint.Forehead,
                XShift = 0.1F,
                YShift = 0.1F,
                Zoom = 1.2F
            };

            bool result = await BotClient.CreateNewStickerSetAsnyc(
                 _classFixture.BotUser.Id,
                 _classFixture.StickerPackName,
                 _classFixture.StickerPackName,
                 stickerFile,
                 Constants.SmilingFaceEmoji,
                 isMasks: true,
                 maskPosition: maskPosition);

            Assert.True(result);
        }
        */

        // ToDo: add more stickers to set
        /*
        [Fact(DisplayName = FactTitles.ShouldAddStickerToSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.AddStickerToSet)]
        [ExecutionOrder(2.3)]
        public async Task Should_Add_Sticker_To_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAddStickerToSet);
            FileToSend stickerFile = new FileToSend(_classFixture.UploadedSticker.FileId);

            bool result = await BotClient.AddStickerToSetAsync(
                 _classFixture.BotUser.Id,
                 _classFixture.StickerPackName,
                 stickerFile,
                 _classFixture.StickerPackEmoji);

            Assert.True(result);
        }
        */

        // ToDo: Create sticker with mask positions
        /*
        [Fact(DisplayName = FactTitles.ShouldSetStickerPositionInSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SetStickerPositionInSet)]
        [ExecutionOrder(2.4)]
        public async Task Should_Should_Set_Sticker_Position_In_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetStickerPositionInSet);

            bool result = await BotClient.SetStickerPositionInSetAsync(
                _classFixture.StickerPackName,
                1);

            Assert.True(result);
        }
        */

        // ToDo: Keep file_id of all sent stickers and delete them from sticker set at the end of tests
        [Fact(DisplayName = FactTitles.ShouldDeleteStickerFromSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.DeleteStickerFromSet)]
        [ExecutionOrder(2.5)]
        public async Task Should_Delete_Sticker_From_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteStickerFromSet);
            string stickerFileId = _classFixture.UploadedSticker.FileId;

            bool result = await BotClient.DeleteStickerFromSetAsync(stickerFileId);

            Assert.True(result);
        }

        #endregion

        private static class FactTitles
        {
            public const string ShouldGetStickerSet = "Should get sticker set";

            public const string ShouldSendSticker = "Should send sticker";

            public const string ShouldUploadStickerFile = "Should upload a sticker file to get file_id";

            public const string ShouldCreateNewStickerSet = "Should create new sticker set with file sent";

            public const string ShouldCreateNewMasksStickerSet = "Should create new masks sticker set with file sent";

            public const string ShouldAddStickerToSet = "Should add sticker to set";

            public const string ShouldSetStickerPositionInSet = "Should set sticker position in set";

            public const string ShouldDeleteStickerFromSet = "Should delete sticker from set";
        }

        private static class Constants
        {
            public const string StickerPackName = "test_by_{0}";

            public const string SmilingFaceEmoji = "\u263A"; // ☺

            public const string FrowningFaceEmoji = "\u2639"; // ☹
        }
    }
}
