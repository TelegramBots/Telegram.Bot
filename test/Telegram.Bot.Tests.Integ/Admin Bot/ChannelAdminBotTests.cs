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

        [Fact(DisplayName = FactTitles.ShouldSetChatTitle)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatTitle)]
        [ExecutionOrder(1)]
        public async Task Should_Set_Chat_Title()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetChatTitle);

            await BotClient.SetChatTitleAsync(
                chatId: _classFixture.ChatId,
                title: _classFixture.ChatTitle
            );
        }

        #endregion

        #region 2. Changing Chat Description

        [Fact(DisplayName = FactTitles.ShouldSetChatDescription)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
        [ExecutionOrder(2.1)]
        public async Task Should_Set_Chat_Description()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetChatDescription);

            await BotClient.SetChatDescriptionAsync(
                chatId: _classFixture.ChatId,
                description: "Test Chat Description"
            );
        }

        [Fact(DisplayName = FactTitles.ShouldDeleteChatDescription)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
        [ExecutionOrder(2.2)]
        public async Task Should_Delete_Chat_Description()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteChatDescription);

            await BotClient.SetChatDescriptionAsync(_classFixture.ChatId);
        }

        #endregion

        #region 3. Pinning Chat Description

        [Fact(DisplayName = FactTitles.ShouldPinMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PinChatMessage)]
        [ExecutionOrder(3.1)]
        public async Task Should_Pin_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldPinMessage);

            Message msg = await BotClient.SendTextMessageAsync(_classFixture.ChatId, "Description to pin");

            await BotClient.PinChatMessageAsync(
                chatId: _classFixture.ChatId,
                messageId: msg.MessageId
            );

            _classFixture.PinnedMessage = msg;
        }

        [Fact(DisplayName = FactTitles.ShouldGetChatPinnedMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        [ExecutionOrder(3.2)]
        public async Task Should_Get_Chat_Pinned_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetChatPinnedMessage);

            Message pinnedMsg = _classFixture.PinnedMessage;

            Chat chat = await BotClient.GetChatAsync(_classFixture.ChatId);

            Assert.True(JToken.DeepEquals(
                JToken.FromObject(pinnedMsg), JToken.FromObject(chat.PinnedMessage)
            ));
        }

        [Fact(DisplayName = FactTitles.ShouldUnpinMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
        [ExecutionOrder(3.3)]
        public async Task Should_Unpin_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUnpinMessage);

            await BotClient.UnpinChatMessageAsync(_classFixture.ChatId);
        }

        [Fact(DisplayName = FactTitles.ShouldGetChatWithNoPinnedMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        [ExecutionOrder(3.4)]
        public async Task Should_Get_Chat_With_No_Pinned_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetChatWithNoPinnedMessage);

            Chat chat = await BotClient.GetChatAsync(_classFixture.ChatId);

            Assert.Null(chat.PinnedMessage);
        }

        #endregion

        #region 4. Changing Chat Photo

        [Fact(DisplayName = FactTitles.ShouldSetChatPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatPhoto)]
        [ExecutionOrder(4.1)]
        public async Task Should_Set_Chat_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetChatPhoto);

            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Logo))
            {
                await BotClient.SetChatPhotoAsync(
                    chatId: _classFixture.ChatId,
                    photo: stream
                );
            }
        }

        [Fact(DisplayName = FactTitles.ShouldGetChatPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        [ExecutionOrder(4.2)]
        public async Task Should_Get_Chat_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetChatPhoto);

            Chat chat = await BotClient.GetChatAsync(_classFixture.ChatId);

            Assert.NotEmpty(chat.Photo.BigFileId);
            Assert.NotEmpty(chat.Photo.SmallFileId);
        }

        [Fact(DisplayName = FactTitles.ShouldDeleteChatPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
        [ExecutionOrder(4.3)]
        public async Task Should_Delete_Chat_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteChatPhoto);

            await BotClient.DeleteChatPhotoAsync(_classFixture.ChatId);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowOnDeletingChatDeletedPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
        [ExecutionOrder(4.4)]
        public async Task Should_Throw_On_Deleting_Chat_Deleted_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowOnDeletingChatDeletedPhoto);

            Exception e = await Assert.ThrowsAnyAsync<Exception>(() =>
                BotClient.DeleteChatPhotoAsync(_classFixture.ChatId));

            Assert.IsType<ApiRequestException>(e);
            Assert.Equal("Bad Request: CHAT_NOT_MODIFIED", e.Message);
        }

        #endregion

        #region 5. Chat Sticker Set

        [Fact(DisplayName = FactTitles.ShouldThrowOnSetChannelStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatStickerSet)]
        [ExecutionOrder(5)]
        public async Task Should_Throw_On_Setting_Chat_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowOnSetChannelStickerSet);

            const string setName = "EvilMinds";

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                _fixture.BotClient.SetChatStickerSetAsync(_classFixture.ChatId, setName)
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

            public const string ShouldThrowOnSetChannelStickerSet =
                "Should throw exception when trying to set sticker set for a channel";
        }

        public class Fixture : AdminBotTestFixture
        {
            public Fixture(TestsFixture fixture)
            {
                ChatId = new ChannelChatFixture(fixture, Constants.TestCollections.ChannelAdminBots).ChannelChatId;
            }
        }
    }

}