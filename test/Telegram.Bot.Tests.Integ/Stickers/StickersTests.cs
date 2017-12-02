using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

// ToDo: Put all "Throws Exception" test cases in another sticker collection

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
            StickerSet stickerSet = await BotClient.GetStickerSetAsync(
                name: setName
            );

            Assert.Equal(setName, stickerSet.Name);
            Assert.Equal("Evil Minds", stickerSet.Title);
            Assert.False(stickerSet.ContainsMasks);
            Assert.NotEmpty(stickerSet.Stickers);
            Assert.True(20 < stickerSet.Stickers.Count);

            _classFixture.StickerSet = stickerSet;
        }

        [Fact(DisplayName = FactTitles.ShouldSendSticker)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendSticker)]
        [ExecutionOrder(1.2)]
        public async Task Should_Send_Sticker()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendSticker);

            Sticker vladDraculaSticker = _classFixture.StickerSet.Stickers[0];

            Message message = await BotClient.SendStickerAsync(
                chatId: _fixture.SuperGroupChatId,
                sticker: new FileToSend(vladDraculaSticker.FileId)
            );

            Assert.Equal(MessageType.StickerMessage, message.Type);
            Assert.Equal(vladDraculaSticker.FileId, message.Sticker.FileId);
            Assert.Equal(vladDraculaSticker.Emoji, message.Sticker.Emoji);
            Assert.Equal(vladDraculaSticker.Height, message.Sticker.Height);
            Assert.Equal(vladDraculaSticker.MaskPosition, message.Sticker.MaskPosition);
            Assert.Equal(vladDraculaSticker.FileSize, message.Sticker.FileSize);
            Assert.Equal(vladDraculaSticker.Thumb.FileId, message.Sticker.Thumb.FileId);
            Assert.Equal(vladDraculaSticker.Thumb.Height, message.Sticker.Thumb.Height);
            Assert.Equal(vladDraculaSticker.Thumb.Width, message.Sticker.Thumb.Width);
        }

        #endregion

        #region 2. Create sticker set

        [Fact(DisplayName = FactTitles.ShouldUploadStickerFile)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.UploadStickerFile)]
        [ExecutionOrder(2.1)]
        public async Task Should_Upload_Stickers()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUploadStickerFile);

            List<File> stickerFiles = new List<File>(2);
            foreach (string pngFile in new[] { "Files/photo/gnu.png", "Files/photo/tux.png" })
            {
                File file;
                using (System.IO.Stream stream = System.IO.File.OpenRead(pngFile))
                {
                    file = await BotClient.UploadStickerFileAsync(
                        userId: _classFixture.OwnerUserId,
                        pngSticker: stream.ToFileToSend(fileName: "sticker")
                    );
                }

                Assert.NotEmpty(file.FileId);
                Assert.True(file.FileSize > 0);

                stickerFiles.Add(file);
            }

            _classFixture.UploadedStickers = stickerFiles;
        }

        [Fact(DisplayName = FactTitles.ShouldThrowInvalidStickerSetNameException)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.2)]
        public async Task Should_Throw_InvalidStickerSetNameException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidStickerSetNameException);

            BadRequestException exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.CreateNewStickerSetAsnyc(
                    userId: _classFixture.OwnerUserId,
                    name: "Test_Sticker_Set",
                    title: "Sticker Set Title",
                    pngSticker: new FileToSend(_classFixture.UploadedStickers.First().FileId),
                    emojis: "😀"
                )
            );

            Assert.IsType<InvalidStickerSetNameException>(exception);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowInvalidStickerEmojisException)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.3)]
        public async Task Should_Throw_InvalidStickerEmojisException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidStickerEmojisException);

            string stickerSetName = $"test_stickers_by_{_fixture.BotUser.Username}";

            BadRequestException exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.CreateNewStickerSetAsnyc(
                    userId: _classFixture.OwnerUserId,
                    name: stickerSetName,
                    title: "Sticker Set Title",
                    pngSticker: new FileToSend(_classFixture.UploadedStickers.First().FileId),
                    emojis: "☺"
                )
            );

            Assert.IsType<InvalidStickerEmojisException>(exception);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowInvalidStickerDimensionsException)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.4)]
        public async Task Should_Throw_InvalidStickerDimensionsException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidStickerDimensionsException);

            string stickerSetName = $"test_stickers_by_{_fixture.BotUser.Username}";

            BadRequestException exception;
            using (System.IO.Stream stream = System.IO.File.OpenRead("Files/photo/logo.png"))
            {
                exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                    BotClient.CreateNewStickerSetAsnyc(
                        userId: _classFixture.OwnerUserId,
                        name: stickerSetName,
                        title: "Sticker Set Title",
                        pngSticker: stream.ToFileToSend("sticker"),
                        emojis: "😁"
                    )
                );
            }

            Assert.IsType<InvalidStickerDimensionsException>(exception);
        }

        [Fact(DisplayName = FactTitles.ShouldCreateNewStickerSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.4)]
        public async Task Should_Create_New_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldCreateNewStickerSet);

            const string stickerSetTitle = "Test Sticker Set";
            string stickerSetName = $"test_by_{_fixture.BotUser.Username}";
            FileToSend gnuStickerFile = new FileToSend(_classFixture.UploadedStickers.First().FileId);
            const string gnuStickerEmoji = "😁";

            bool result;
            try
            {
                result = await BotClient.CreateNewStickerSetAsnyc(
                    userId: _classFixture.OwnerUserId,
                    name: stickerSetName,
                    title: stickerSetTitle,
                    pngSticker: gnuStickerFile,
                    emojis: gnuStickerEmoji
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Assert.True(result);

            //Assert.Equal("Bad Request: sticker set name is already occupied", e.Message);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowStickerSetNameExistsException)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(5.3)]
        public async Task Should_Throw_StickerSetNameExistsException2()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowStickerSetNameExistsException);

            FileToSend stickerFile = new FileToSend(_classFixture.UploadedStickers.First().FileId);
            const string stickerSetName = "Test_Sticker_Set";
            const string stickerSetTitle = "Title_" + stickerSetName;

            BadRequestException exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.CreateNewStickerSetAsnyc(
                    userId: _classFixture.OwnerUserId,
                    name: stickerSetName,
                    title: stickerSetTitle,
                    pngSticker: stickerFile,
                    emojis: "😀"
                )
            );

            Assert.IsType<InvalidStickerSetNameException>(exception);

        }

        //[Fact(DisplayName = FactTitles.ShouldCreateNewStickerSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(5.2)]
        public async Task Should_Create_New_Sticker_Set2()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldCreateNewStickerSet);

            try
            {
                FileToSend stickerFile = new FileToSend(_classFixture.UploadedStickers.First().FileId);
                _classFixture.StickerSetName = $"stickers_test_by_{_fixture.BotUser.Username}";

                bool result = await BotClient.CreateNewStickerSetAsnyc(
                     _classFixture.OwnerUserId,
                     _classFixture.StickerSetName,
                     _classFixture.StickerSetName,
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

        // ToDo: Create sticker with mask positions
        /*
        [Fact(DisplayName = FactTitles.ShouldCreateNewMasksStickerSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.3)]
        public async Task Should_Create_New_Masks_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldCreateNewMasksStickerSet);
            FileToSend stickerFile = new FileToSend(_classFixture.UploadedSticker.FileId);
            _classFixture.StickerSetName = $"masks_test_by_{_classFixture.BotUser.Username}";
            MaskPosition maskPosition = new MaskPosition()
            {
                Point = MaskPositionPoint.Forehead,
                XShift = 0.1F,
                YShift = 0.1F,
                Zoom = 1.2F
            };

            bool result = await BotClient.CreateNewStickerSetAsnyc(
                 _classFixture.BotUser.Id,
                 _classFixture.StickerSetName,
                 _classFixture.StickerSetName,
                 stickerFile,
                 Constants.SmilingFaceEmoji,
                 isMasks: true,
                 maskPosition: maskPosition);

            Assert.True(result);
        }
        */


        // ToDo: add more stickers to set
        [Fact(DisplayName = FactTitles.ShouldAddStickerToSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.AddStickerToSet)]
        [ExecutionOrder(5.3)]
        public async Task Should_Add_Sticker_To_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAddStickerToSet);
            FileToSend stickerFile = new FileToSend(_classFixture.UploadedStickers.First().FileId);

            bool result = await BotClient.AddStickerToSetAsync(
                 _classFixture.OwnerUserId,
                 _classFixture.StickerSetName,
                 stickerFile,
                 Constants.ThinkingFaceEmoji);
            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldSetStickerPositionInSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SetStickerPositionInSet)]
        [ExecutionOrder(5.4)]
        public async Task Should_Should_Set_Sticker_Position_In_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetStickerPositionInSet);

            bool result = await BotClient.SetStickerPositionInSetAsync(
                _classFixture.UploadedStickers.First().FileId,
                0);

            Assert.True(result);
        }

        // ToDo: Keep file_id of all sent stickers and delete them from sticker set at the end of tests
        /*
        [Fact(DisplayName = FactTitles.ShouldDeleteStickerFromSet)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.DeleteStickerFromSet)]
        [ExecutionOrder(2.5)]
        public async Task Should_Delete_Sticker_From_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteStickerFromSet).ConfigureAwait(false);

            foreach (var stickerFile in _classFixture.UploadedStickers)
            {
                try
                {
                    string stickerFileId = stickerFile.FileId;

                    bool result = await BotClient.DeleteStickerFromSetAsync(stickerFileId).ConfigureAwait(false);

                    Assert.True(result);
                }
                catch (ApiRequestException e) { }
            }
        }
        */
        #endregion

        private static class FactTitles
        {
            public const string ShouldGetStickerSet = "Should get sticker set";

            public const string ShouldSendSticker = "Should send sticker of Vlad the Impaler 🍷";

            public const string ShouldUploadStickerFile = "Should upload a sticker file to get file_id";

            public const string ShouldThrowInvalidStickerSetNameException =
                "Should throw InvalidStickerSetNameException while trying to create sticker set with name not ending in _by_<bot username>";

            public const string ShouldThrowInvalidStickerEmojisException =
                "Should throw InvalidStickerEmojisException while trying to create sticker with invalid emoji";

            public const string ShouldThrowInvalidStickerDimensionsException =
                "Should throw InvalidStickerDimensionsException while trying to create sticker with invalid dimensions";

            public const string ShouldCreateNewStickerSet = "Should create new sticker set with file sent";

            public const string ShouldThrowStickerSetNameExistsException = "Should throw StickerSetNameExistsException while trying to create sticker set with the same name";

            public const string ShouldCreateNewMasksStickerSet = "Should create new masks sticker set with file sent";

            public const string ShouldAddStickerToSet = "Should add sticker to set";

            public const string ShouldSetStickerPositionInSet = "Should set sticker position in set";

            public const string ShouldDeleteStickerFromSet = "Should delete sticker from set";
        }

        private static class Constants
        {
            //public const string StickerSetName = "test_by_{0}";

            public const string SmilingFaceEmoji = "😀";

            public const string ThinkingFaceEmoji = "🤔";
        }
    }
}
