using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
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

        #region 1. Create sticker set

        [Fact(DisplayName = FactTitles.ShouldUploadStickerFile)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.UploadStickerFile)]
        [ExecutionOrder(1.1)]
        public async Task Should_Upload_Stickers()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUploadStickerFile);
            var stikers = new Dictionary<string, string>()
            {
                ["lincoln"] = "Files/photo/lincoln.png",
                ["einstein"] = "Files/photo/einstein.png"
            };

            foreach (var stickerName in stikers.Keys)
            {
                File file;
                using (var stream = System.IO.File.OpenRead(stikers[stickerName]))
                {
                    file = await BotClient.UploadStickerFileAsync(
                        _classFixture.ChatId,
                        stream.ToFileToSend(stickerName)
                    ).ConfigureAwait(false);
                }

                Assert.NotEmpty(file.FileId);
                Assert.True(file.FileSize > 0);

                _classFixture.UploadedStickers.Add(file);
            }
        }

        [Fact(DisplayName = FactTitles.ShouldCreateNewStickerSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(1.2)]
        public async Task Should_Create_New_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldCreateNewStickerSet);

            try
            {
                var stickerFile = new FileToSend(_classFixture.UploadedStickers[0].FileId);
                var stickerPackName = string.Format(Constants.StickerPackName, _fixture.BotUser.Username);
                var stickerPackTitle = string.Format(Constants.StickerPackTitle, _fixture.BotUser.Username);

                bool result = await BotClient.CreateNewStickerSetAsnyc(
                    _classFixture.ChatId,
                    stickerPackName,
                    stickerPackTitle,
                    stickerFile,
                    Constants.SmilingFaceEmoji);

                Assert.True(result);
            }
            catch (ApiRequestException e)
            {
                Assert.Equal(400, e.ErrorCode);
                Assert.Equal("Bad Request: sticker set name is already occupied", e.Message);
            }
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

        [Fact(DisplayName = FactTitles.ShouldAddStickerToSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.AddStickerToSet)]
        [ExecutionOrder(1.4)]
        public async Task Should_Add_Sticker_To_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAddStickerToSet);

            foreach (var sticker in _classFixture.UploadedStickers)
            {
                var stickerFile = new FileToSend(sticker.FileId);
                var stickerPackName = string.Format(Constants.StickerPackName, _fixture.BotUser.Username);

                bool result = await BotClient.AddStickerToSetAsync(
                    _classFixture.ChatId,
                    stickerPackName,
                    stickerFile,
                    Constants.ThinkingFaceEmoji);

                Assert.True(result);
            }
            await Task.Delay(30_000);
        }

        #endregion

        #region 2. Get and send stickers from a set

        [Fact(DisplayName = FactTitles.ShouldGetStickerSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.GetStickerSet)]
        [ExecutionOrder(2.1)]
        public async Task Should_Get_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetStickerSet);
            var stickerPackName = string.Format(Constants.StickerPackName, _fixture.BotUser.Username);
            var stickerPackTitle = string.Format(Constants.StickerPackTitle, _fixture.BotUser.Username);

            StickerSet stickerSet = await BotClient.GetStickerSetAsync(stickerPackName);

            Assert.Equal(stickerPackName, stickerSet.Name);
            Assert.Equal(stickerPackTitle, stickerSet.Title);
            Assert.False(stickerSet.ContainsMasks);
            Assert.NotEmpty(stickerSet.Stickers);
            Assert.True(2 <= stickerSet.Stickers.Count);
            // ToDo: Add more asserts

            _classFixture.StickerSet = stickerSet;
            await BotClient.SendTextMessageAsync(
                _classFixture.TesterPrivateChatId,
                $"User sticker set: https://t.me/addstickers/{stickerSet.Name}");
        }

        [Fact(DisplayName = FactTitles.ShouldSendSticker)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendSticker)]
        [ExecutionOrder(2.2)]
        public async Task Should_Send_Sticker()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendSticker);

            Assert.NotNull(_classFixture.StickerSet);

            var sticker = new FileToSend(_classFixture.StickerSet.Stickers[0].FileId);

            Message message = await _fixture.BotClient.SendStickerAsync(_classFixture.TesterPrivateChatId, sticker);

            Assert.Equal(MessageType.StickerMessage, message.Type);
            Assert.Equal(sticker.FileId, message.Sticker.FileId);
        }

        #endregion

        #region 3. Set position and delete stickers from a set

        [Fact(DisplayName = FactTitles.ShouldSetStickerPositionInSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SetStickerPositionInSet)]
        [ExecutionOrder(3.1)]
        public async Task Should_Should_Set_Sticker_Position_In_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetStickerPositionInSet);

            Assert.NotNull(_classFixture.StickerSet);

            var sticker = _classFixture.StickerSet.Stickers[1].FileId;

            bool result = await BotClient.SetStickerPositionInSetAsync(
                sticker,
                0);

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldDeleteStickerFromSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.DeleteStickerFromSet)]
        [ExecutionOrder(3.2)]
        public async Task Should_Delete_Sticker_From_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteStickerFromSet).ConfigureAwait(false);

            Assert.NotNull(_classFixture.StickerSet);

            foreach (var sticker in _classFixture.StickerSet.Stickers)
            {
                bool result = await BotClient.DeleteStickerFromSetAsync(sticker.FileId).ConfigureAwait(false);

                Assert.True(result);
            }
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
            public const string StickerPackName = "stickers_test_2_by_{0}";

            public const string StickerPackTitle = "Stickers Test by {0}";

            public const string SmilingFaceEmoji = "😀";

            public const string ThinkingFaceEmoji = "🤔";
        }
    }
}
