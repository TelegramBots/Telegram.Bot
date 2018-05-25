using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using File = Telegram.Bot.Types.File;

// ToDo: Put all "Throws Exception" test cases in another sticker collection

namespace Telegram.Bot.Tests.Integ.Stickers
{
    [Collection(Constants.TestCollections.Stickers)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class StickersTests : IClassFixture<StickersTestsFixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly StickersTestsFixture _classFixture;

        private readonly TestsFixture _fixture;

        public StickersTests(TestsFixture fixture, StickersTestsFixture classFixture)
        {
            _classFixture = classFixture;
            _fixture = fixture;
        }

        #region 1. Get and send stickers from a set

        [OrderedFact(DisplayName = FactTitles.ShouldGetStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
        public async Task Should_Get_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetStickerSet);

            const string setName = "EvilMinds";
            const string setTitle = "Evil Minds";
            StickerSet stickerSet = await BotClient.GetStickerSetAsync(
                name: setName
            );

            Assert.Equal(setName, stickerSet.Name);
            Assert.Equal(setTitle, stickerSet.Title);
            Assert.False(stickerSet.ContainsMasks);
            Assert.NotEmpty(stickerSet.Stickers);
            Assert.True(20 < stickerSet.Stickers.Length);

            _classFixture.EvilMindsStickerSet = stickerSet;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetStickerSetWithMasks)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
        public async Task Should_Get_Sticker_Set_With_Masks()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetStickerSetWithMasks);

            const string setName = "masks2";
            const string setTitle = "Masks II: Face Lift";
            StickerSet stickerSet = await BotClient.GetStickerSetAsync(
                name: setName
            );

            Assert.Equal(setName, stickerSet.Name);
            Assert.Equal(setTitle, stickerSet.Title);
            Assert.True(stickerSet.ContainsMasks);
            Assert.True(20 < stickerSet.Stickers.Length);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendSticker)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendSticker)]
        public async Task Should_Send_Sticker()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendSticker);

            Sticker vladDraculaSticker = _classFixture.EvilMindsStickerSet.Stickers[0];

            Message message = await BotClient.SendStickerAsync(
                chatId: _fixture.SupergroupChat.Id,
                sticker: vladDraculaSticker.FileId
            );

            Assert.Equal(MessageType.Sticker, message.Type);
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

        [OrderedFact(DisplayName = FactTitles.ShouldUploadStickerFile)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UploadStickerFile)]
        public async Task Should_Upload_Stickers()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUploadStickerFile);

            List<File> stickerFiles = new List<File>(2);
            foreach (string pngFile in new[] {Constants.FileNames.Photos.Gnu, Constants.FileNames.Photos.Tux})
            {
                File file;
                using (System.IO.Stream stream = System.IO.File.OpenRead(pngFile))
                {
                    file = await BotClient.UploadStickerFileAsync(
                        userId: _classFixture.OwnerUserId,
                        pngSticker: stream
                    );
                }

                Assert.NotEmpty(file.FileId);
                Assert.True(file.FileSize > 0);

                stickerFiles.Add(file);
            }

            _classFixture.UploadedStickers = stickerFiles;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowInvalidStickerSetNameException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
        public async Task Should_Throw_InvalidStickerSetNameException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidStickerSetNameException);

            BadRequestException exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.CreateNewStickerSetAsync(
                    userId: _classFixture.OwnerUserId,
                    name: "Invalid_Sticker_Set_Name",
                    title: "Sticker Set Title",
                    pngSticker: _classFixture.UploadedStickers.First().FileId,
                    emojis: "😀"
                )
            );

            Assert.IsType<InvalidStickerSetNameException>(exception);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowInvalidStickerEmojisException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
        public async Task Should_Throw_InvalidStickerEmojisException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidStickerEmojisException);

            BadRequestException exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.CreateNewStickerSetAsync(
                    userId: _classFixture.OwnerUserId,
                    name: "valid_name3" + _classFixture.TestStickerSetName,
                    title: "Sticker Set Title",
                    pngSticker: _classFixture.UploadedStickers.First().FileId,
                    emojis: "INVALID"
                )
            );

            Assert.IsType<InvalidStickerEmojisException>(exception);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowInvalidStickerDimensionsException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
        public async Task Should_Throw_InvalidStickerDimensionsException()
        {
            // ToDo exception when sending jpeg file. Bad Request: STICKER_PNG_NOPNG
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidStickerDimensionsException);

            BadRequestException exception;
            using (System.IO.Stream stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Logo))
            {
                exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                    BotClient.CreateNewStickerSetAsync(
                        userId: _classFixture.OwnerUserId,
                        name: "valid_name2" + _classFixture.TestStickerSetName,
                        title: "Sticker Set Title",
                        pngSticker: stream,
                        emojis: "😁"
                    )
                );
            }

            Assert.IsType<InvalidStickerDimensionsException>(exception);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldCreateNewStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
        public async Task Should_Create_New_Sticker_Set()
        {
            Message testCaseNotifMessage = await _fixture.SendTestCaseNotificationAsync(
                FactTitles.ShouldCreateNewStickerSet,
                "You can add new sticker set using the link: " +
                $"t.me/addstickers/{_classFixture.TestStickerSetName.Replace("_", @"\_")}"
            );

            string gnuStickerFileId = _classFixture.UploadedStickers.First().FileId;

            bool result = false;
            try
            {
                await BotClient.CreateNewStickerSetAsync(
                    userId: _classFixture.OwnerUserId,
                    name: _classFixture.TestStickerSetName,
                    title: "Test Sticker Set",
                    pngSticker: gnuStickerFileId,
                    emojis: "😁"
                );
            }
            catch (StickerSetNameExistsException)
            {
                // ToDo: Could wait for tester to remove the set and click on "Continue" key reply markup, then retry

                await BotClient.SendTextMessageAsync(
                    chatId: _fixture.SupergroupChat.Id,
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

        [OrderedFact(DisplayName = FactTitles.ShouldThrowStickerSetNameExistsException,
            Skip = "Upstream bug in Bot API")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateNewStickerSet)]
        public async Task Should_Throw_StickerSetNameExistsException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowStickerSetNameExistsException);

            BadRequestException exception;
            using (System.IO.Stream stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Ruby))
            {
                exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                    BotClient.CreateNewStickerSetAsync(
                        userId: _classFixture.OwnerUserId,
                        name: _classFixture.TestStickerSet.Name,
                        title: "Another Test Sticker Set",
                        pngSticker: stream,
                        emojis: "😎"
                    )
                );
            }

            Assert.IsType<StickerSetNameExistsException>(exception);
        }

        #endregion

        #region 3. Edit sticker set

        [OrderedFact(DisplayName = FactTitles.ShouldAddStickerFileToSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AddStickerToSet)]
        public async Task Should_Add_Uploaded_Sticker_File_To_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAddStickerFileToSet);

            await BotClient.AddStickerToSetAsync(
                userId: _classFixture.OwnerUserId,
                name: _classFixture.TestStickerSet.Name,
                pngSticker: _classFixture.UploadedStickers.Last().FileId,
                emojis: "😏😃"
            );
        }

        [OrderedFact(DisplayName = FactTitles.ShouldAddStickerWithMaskPositionToSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AddStickerToSet)]
        public async Task Should_Add_Sticker_With_Mask_Position_To_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAddStickerWithMaskPositionToSet);

            using (System.IO.Stream stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Vlc))
            {
                await BotClient.AddStickerToSetAsync(
                    userId: _classFixture.OwnerUserId,
                    name: _classFixture.TestStickerSet.Name,
                    pngSticker: stream,
                    emojis: "😇",
                    maskPosition: new MaskPosition
                    {
                        Point = MaskPositionPoint.Forehead,
                        Scale = .8f
                    }
                );
            }
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSetStickerPositionInSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetStickerPositionInSet)]
        public async Task Should_Change_Sticker_Position_In_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetStickerPositionInSet);

            StickerSet testStickerSet = await BotClient.GetStickerSetAsync(_classFixture.TestStickerSet.Name);
            Sticker tuxSticker = testStickerSet.Stickers[1];

            await BotClient.SetStickerPositionInSetAsync(
                sticker: tuxSticker.FileId,
                position: 0
            );
        }

        /// <remarks>
        /// One sticker in the set is not removed because removing last sticker would cause the sticker set to be removed
        /// and bots cannot remove a sticker set.
        /// </remarks>
        [OrderedFact(DisplayName = FactTitles.ShouldRemoveStickersFromSet, Skip = "Upstream bug in Bot API")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteStickerFromSet)]
        public async Task Should_Remove_All_Stickers_From_Set_Except_1()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldRemoveStickersFromSet);

            StickerSet testStickerSet = await BotClient.GetStickerSetAsync(_classFixture.TestStickerSet.Name);
            Sticker[] stickersToRemove = testStickerSet.Stickers.SkipLast(1).ToArray();
            foreach (Sticker sticker in stickersToRemove)
            {
                // Wait a few seconds because Bot API delays in updating changes to sticker sets
                await Task.Delay(1_000);

                await BotClient.DeleteStickerFromSetAsync(sticker: sticker.FileId);
            }
        }

        [OrderedFact(DisplayName = FactTitles.ShouldStickerSetNotModifiedException,
            Skip = "Not all the stickers will be removed in previous test")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteStickerFromSet)]
        public async Task Should_Throw_StickerSetNotModifiedException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldStickerSetNotModifiedException);

            StickerSet testStickerSet = await BotClient.GetStickerSetAsync(_classFixture.TestStickerSet.Name);

            BadRequestException exception = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.DeleteStickerFromSetAsync(
                    sticker: testStickerSet.Stickers.Single().FileId
                )
            );

            Assert.IsType<StickerSetNotModifiedException>(exception);
        }

        #endregion

        private static class FactTitles
        {
            public const string ShouldGetStickerSet = "Should get EvilMinds sticker set";

            public const string ShouldGetStickerSetWithMasks = "Should get Masks2 sticker set";

            public const string ShouldSendSticker = "Should send sticker of Vlad the Impaler 🍷";

            public const string ShouldUploadStickerFile = "Should upload sticker files to get file_id";

            public const string ShouldThrowInvalidStickerSetNameException =
                "Should throw InvalidStickerSetNameException while trying to create sticker set with name not ending in _by_<bot username>";

            public const string ShouldThrowInvalidStickerEmojisException =
                "Should throw InvalidStickerEmojisException while trying to create sticker with invalid emoji";

            public const string ShouldThrowInvalidStickerDimensionsException =
                "Should throw InvalidStickerDimensionsException while trying to create sticker with invalid dimensions";

            public const string ShouldCreateNewStickerSet = "Should create new sticker set with the file sent";

            public const string ShouldThrowStickerSetNameExistsException =
                "Should throw StickerSetNameExistsException while trying to create sticker set with the same name";

            public const string ShouldAddStickerFileToSet = "Should add Tux sticker to set using its uploaded file_id";

            public const string ShouldAddStickerWithMaskPositionToSet =
                "Should add VLC logo sticker with mask position like hat on forehead";

            public const string ShouldSetStickerPositionInSet = "Should set Tux in first position of sticker set";

            public const string ShouldRemoveStickersFromSet =
                "Should remove all stickers from test sticker set except VLC's logo";

            public const string ShouldStickerSetNotModifiedException =
                "Should throw StickerSetNotModifiedException while trying to remove the last sticker in the set";
        }
    }
}
