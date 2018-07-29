using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Admin_Bot
{
    [Collection(Constants.TestCollections.ChannelAdminBots)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ChannelAdminBotTests : IClassFixture<ChannelAdminBotTests.Fixture>
    {
        private readonly AdminBotTestFixture _classFixture;

        private readonly TestsFixture _fixture;

        private ITelegramBotClient BotClient => _fixture.BotClient;

        public ChannelAdminBotTests(TestsFixture testsFixture, Fixture classFixture)
        {
            _fixture = testsFixture;
            _classFixture = classFixture;
        }

        #region 1. Changing Chat Title

        [OrderedFact(DisplayName = FactTitles.ShouldSetChatTitle)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatTitle)]
        public async Task Should_Set_Chat_Title()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetChatTitle);

            await BotClient.SetChatTitleAsync(
                chatId: _classFixture.Chat.Id,
                title: _classFixture.ChatTitle
            );
        }

        #endregion

        #region 2. Changing Chat Description

        [OrderedFact(DisplayName = FactTitles.ShouldSetChatDescription)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
        public async Task Should_Set_Chat_Description()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetChatDescription);

            await BotClient.SetChatDescriptionAsync(
                chatId: _classFixture.Chat.Id,
                description: "Test Chat Description"
            );
        }

        [OrderedFact(DisplayName = FactTitles.ShouldDeleteChatDescription)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
        public async Task Should_Delete_Chat_Description()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteChatDescription);

            await BotClient.SetChatDescriptionAsync(_classFixture.Chat.Id);
        }

        #endregion

        #region 3. Pinning Chat Description

        [OrderedFact(DisplayName = FactTitles.ShouldPinMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PinChatMessage)]
        public async Task Should_Pin_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldPinMessage);

            Message msg = await BotClient.SendTextMessageAsync(_classFixture.Chat.Id, "Description to pin");

            await BotClient.PinChatMessageAsync(
                chatId: _classFixture.Chat.Id,
                messageId: msg.MessageId,
                disableNotification: true
            );

            _classFixture.PinnedMessage = msg;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetChatPinnedMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        public async Task Should_Get_Chat_Pinned_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetChatPinnedMessage);

            Message pinnedMsg = _classFixture.PinnedMessage;

            Chat chat = await BotClient.GetChatAsync(_classFixture.Chat.Id);

            Assert.True(JToken.DeepEquals(
                JToken.FromObject(pinnedMsg), JToken.FromObject(chat.PinnedMessage)
            ));
        }

        [OrderedFact(DisplayName = FactTitles.ShouldUnpinMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
        public async Task Should_Unpin_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUnpinMessage);

            await BotClient.UnpinChatMessageAsync(_classFixture.Chat.Id);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetChatWithNoPinnedMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        public async Task Should_Get_Chat_With_No_Pinned_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetChatWithNoPinnedMessage);

            Chat chat = await BotClient.GetChatAsync(_classFixture.Chat.Id);

            Assert.Null(chat.PinnedMessage);
        }

        #endregion

        #region 4. Changing Chat Photo

        [OrderedFact(DisplayName = FactTitles.ShouldSetChatPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatPhoto)]
        public async Task Should_Set_Chat_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetChatPhoto);

            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Logo))
            {
                await BotClient.SetChatPhotoAsync(
                    chatId: _classFixture.Chat.Id,
                    photo: stream
                );
            }
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetChatPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        public async Task Should_Get_Chat_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetChatPhoto);

            Chat chat = await BotClient.GetChatAsync(_classFixture.Chat.Id);

            Assert.NotEmpty(chat.Photo.BigFileId);
            Assert.NotEmpty(chat.Photo.SmallFileId);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldDeleteChatPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
        public async Task Should_Delete_Chat_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteChatPhoto);

            await BotClient.DeleteChatPhotoAsync(_classFixture.Chat.Id);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowOnDeletingChatDeletedPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
        public async Task Should_Throw_On_Deleting_Chat_Deleted_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowOnDeletingChatDeletedPhoto);

            Exception e = await Assert.ThrowsAnyAsync<Exception>(() =>
                BotClient.DeleteChatPhotoAsync(_classFixture.Chat.Id));

            Assert.IsType<ApiRequestException>(e);
            Assert.Equal("Bad Request: CHAT_NOT_MODIFIED", e.Message);
        }

        /// <summary>
        /// If chat had a photo before, reset the photo back.
        /// </summary>
        [OrderedFact(DisplayName = FactTitles.ShouldResetOldChatPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatPhoto)]
        public async Task Should_Reset_Old_Chat_Photo_If_Existed()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldResetOldChatPhoto);

            // "Chat.Photo" might be null if there is no photo currently set
            string previousChatPhotoId = _classFixture.Chat.Photo?.BigFileId;
            if (previousChatPhotoId == default)
            {
                // chat didn't have a photo
                return;
            }

            using (Stream photoStream = new MemoryStream())
            {
                // pass photo's file_id, prepare file for download, and download the file into memroy
                await BotClient.GetInfoAndDownloadFileAsync(previousChatPhotoId, photoStream);

                // need to set position of memory stream back to its start so next method reads photo stream from the beginning
                photoStream.Position = 0;

                await BotClient.SetChatPhotoAsync(
                    chatId: _classFixture.Chat.Id,
                    photo: photoStream
                );
            }
        }

        #endregion

        #region 5. Chat Sticker Set

        [OrderedFact(DisplayName = FactTitles.ShouldThrowOnSetChannelStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatStickerSet)]
        public async Task Should_Throw_On_Setting_Chat_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowOnSetChannelStickerSet);

            const string setName = "EvilMinds";

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                _fixture.BotClient.SetChatStickerSetAsync(_classFixture.Chat.Id, setName)
            );

            // ToDo: Create exception type
            Assert.Equal(400, exception.ErrorCode);
            Assert.Equal("Bad Request: method is available only for supergroups", exception.Message);
        }

        #endregion

        private static class FactTitles
        {
            public const string ShouldSetChatTitle = "Should set chat title";

            public const string ShouldSetChatDescription = "Should set chat description";

            public const string ShouldDeleteChatDescription = "Should delete chat description";

            public const string ShouldPinMessage = "Should pin chat message";

            public const string ShouldGetChatPinnedMessage = "Should get chat's pinned message";

            public const string ShouldUnpinMessage = "Should unpin chat message";

            public const string ShouldGetChatWithNoPinnedMessage = "Should get the chat info without a pinned message";

            public const string ShouldSetChatPhoto = "Should set chat photo";

            public const string ShouldGetChatPhoto = "Should get chat photo";

            public const string ShouldDeleteChatPhoto = "Should delete chat photo";

            public const string ShouldThrowOnDeletingChatDeletedPhoto =
                "Should throw exception in deleting chat photo with no photo currently set";

            public const string ShouldResetOldChatPhoto = "Should reset the same old chat photo if existed";

            public const string ShouldThrowOnSetChannelStickerSet =
                "Should throw exception when trying to set sticker set for a channel";
        }

        public class Fixture : AdminBotTestFixture
        {
            public Fixture(TestsFixture fixture)
            {
                Chat = new ChannelChatFixture(fixture, Constants.TestCollections.ChannelAdminBots).ChannelChat;
            }
        }
    }
}
