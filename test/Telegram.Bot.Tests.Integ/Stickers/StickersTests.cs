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
    [Collection(Constants.TestCollections.Stickers)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
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
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
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

            _classFixture.EvilMindsStickerSet = stickerSet;
        }

        [Fact(DisplayName = FactTitles.ShouldSendSticker)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendSticker)]
        [ExecutionOrder(1.2)]
        public async Task Should_Send_Sticker()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendSticker);

            Sticker vladDraculaSticker = _classFixture.EvilMindsStickerSet.Stickers[0];

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
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UploadStickerFile)]
        [ExecutionOrder(2.1)]
        public async Task Should_Upload_Stickers()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUploadStickerFile);

            List<File> stickerFiles = new List<File>(2);
            foreach (string pngFile in new[] { Constants.FileNames.Photos.Gnu, Constants.FileNames.Photos.Tux })
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
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.2)]
        public async Task Should_Throw_InvalidStickerSetNameException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidStickerSetNameException);

            BadRequestException exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.CreateNewStickerSetAsnyc(
                    userId: _classFixture.OwnerUserId,
                    name: "Invalid_Sticker_Set_Name",
                    title: "Sticker Set Title",
                    pngSticker: new FileToSend(_classFixture.UploadedStickers.First().FileId),
                    emojis: "😀"
                )
            );

            Assert.IsType<InvalidStickerSetNameException>(exception);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowInvalidStickerEmojisException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.3)]
        public async Task Should_Throw_InvalidStickerEmojisException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidStickerEmojisException);

            BadRequestException exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.CreateNewStickerSetAsnyc(
                    userId: _classFixture.OwnerUserId,
                    name: "valid_name" + _classFixture.TestStickerSetName,
                    title: "Sticker Set Title",
                    pngSticker: new FileToSend(_classFixture.UploadedStickers.First().FileId),
                    emojis: "☺"
                )
            );

            Assert.IsType<InvalidStickerEmojisException>(exception);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowInvalidStickerDimensionsException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.4)]
        public async Task Should_Throw_InvalidStickerDimensionsException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidStickerDimensionsException);

            BadRequestException exception;
            using (System.IO.Stream stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Logo))
            {
                exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                    BotClient.CreateNewStickerSetAsnyc(
                        userId: _classFixture.OwnerUserId,
                        name: "valid_name" + _classFixture.TestStickerSetName,
                        title: "Sticker Set Title",
                        pngSticker: stream.ToFileToSend("sticker"),
                        emojis: "😁"
                    )
                );
            }

            Assert.IsType<InvalidStickerDimensionsException>(exception);
        }

        [Fact(DisplayName = FactTitles.ShouldCreateNewStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
        [ExecutionOrder(2.5)]
        public async Task Should_Create_New_Sticker_Set()
        {
            Message testCaseNotifMessage =
                await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldCreateNewStickerSet);

            FileToSend gnuStickerFile = new FileToSend(_classFixture.UploadedStickers.First().FileId);

            bool result;
            try
            {
                result = await BotClient.CreateNewStickerSetAsnyc(
                    userId: _classFixture.OwnerUserId,
                    name: _classFixture.TestStickerSetName,
                    title: "Test Sticker Set",
                    pngSticker: gnuStickerFile,
                    emojis: "😁"
                );
            }
            catch (StickerSetNameExistsException)
            {
                // ToDo: Could wait for tester to remove the set and click on "Continue" key reply markup, then retry

                await BotClient.SendTextMessageAsync(
                    chatId: _fixture.SuperGroupChatId,
                    text: $"😕 Sticker set `{_classFixture.TestStickerSetName}` already exists. " +
                          "Tester should remove it using @Stickers bot and run this test again.",
                    parseMode: ParseMode.Markdown,
                    replyToMessageId: testCaseNotifMessage.MessageId
                );

                result = true;
            }

            Assert.True(result);

            _classFixture.TestStickerSet = await BotClient.GetStickerSetAsync(_classFixture.TestStickerSetName);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowStickerSetNameExistsException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.6)]
        public async Task Should_Throw_StickerSetNameExistsException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowStickerSetNameExistsException);

            BadRequestException exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.CreateNewStickerSetAsnyc(
                        userId: _classFixture.OwnerUserId,
                        name: _classFixture.TestStickerSet.Name,
                        title: "Test Sticker Set",
                        pngSticker: new FileToSend(_classFixture.TestStickerSet.Stickers.Last().FileId),
                        emojis: "😁"
                    )
            );

            Assert.IsType<StickerSetNameExistsException>(exception);
        }

        #endregion

        #region 3. Edit sticker set

        [Fact(DisplayName = FactTitles.ShouldAddStickerFileToSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AddStickerToSet)]
        [ExecutionOrder(3.1)]
        public async Task Should_Add_Uploaded_Sticker_File_To_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAddStickerFileToSet);

            bool result = await BotClient.AddStickerToSetAsync(
                userId: _classFixture.OwnerUserId,
                name: _classFixture.TestStickerSet.Name,
                pngSticker: new FileToSend(fileId: _classFixture.UploadedStickers.Last().FileId),
                emojis: "😏😃"
            );

            Assert.True(result);
        }

        // ToDo: 3.2: add a sticker from EvilMinds to Test set

        //[Fact(DisplayName = FactTitles.ShouldSetStickerPositionInSet)]
        //[Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetStickerPositionInSet)]
        //[ExecutionOrder(3.3)]
        //public async Task Should_Should_Set_Sticker_Position_In_Set()
        //{
        //    await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetStickerPositionInSet);

        //    bool result = await BotClient.SetStickerPositionInSetAsync(
        //        _classFixture.UploadedStickers.First().FileId,
        //        0);

        //    Assert.True(result);
        //}

        [Fact(DisplayName = FactTitles.ShouldRemoveStickersFromSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteStickerFromSet)]
        [ExecutionOrder(3.5)]
        public async Task Remove_All_Stickers_From_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldRemoveStickersFromSet);

            foreach (Sticker sticker in _classFixture.TestStickerSet.Stickers)
            {
                bool result = await BotClient.DeleteStickerFromSetAsync(
                    sticker: sticker.FileId
                );

                Assert.True(result);
            }
        }

        #endregion

        // ToDo: Create sticker with mask positions
        /*
        [Fact(DisplayName = FactTitles.ShouldCreateNewMasksStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
        [ExecutionOrder(2.3)]
        public async Task Should_Create_New_Masks_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldCreateNewMasksStickerSet);
            FileToSend stickerFile = new FileToSend(_classFixture.UploadedSticker.FileId);
            _classFixture.TestStickerSetName = $"masks_test_by_{_classFixture.BotUser.Username}";
            MaskPosition maskPosition = new MaskPosition()
            {
                Point = MaskPositionPoint.Forehead,
                XShift = 0.1F,
                YShift = 0.1F,
                Zoom = 1.2F
            };

            bool result = await BotClient.CreateNewStickerSetAsnyc(
                 _classFixture.BotUser.Id,
                 _classFixture.TestStickerSetName,
                 _classFixture.TestStickerSetName,
                 stickerFile,
                 Constants.SmilingFaceEmoji,
                 isMasks: true,
                 maskPosition: maskPosition);

            Assert.True(result);
        }

        // ToDo: Keep file_id of all sent stickers and delete them from sticker set at the end of tests
        /*
        [Fact(DisplayName = FactTitles.ShouldRemoveStickersFromSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteStickerFromSet)]
        [ExecutionOrder(2.5)]
        public async Task Should_Delete_Sticker_From_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldRemoveStickersFromSet).ConfigureAwait(false);

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

            public const string ShouldCreateNewStickerSet = "Should create new sticker set with the file sent";

            public const string ShouldThrowStickerSetNameExistsException = "Should throw StickerSetNameExistsException while trying to create sticker set with the same name";

            public const string ShouldAddStickerFileToSet = "Should add Tux sticker to set using its uploaded file_id";

            public const string ShouldRemoveStickersFromSet = "Should remove all stickers from test sticker set";

            //public const string ShouldCreateNewMasksStickerSet = "Should create new masks sticker set with file sent";


            //public const string ShouldSetStickerPositionInSet = "Should set sticker position in set";

        }
    }
}
