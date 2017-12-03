using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.AdminBots
{
    [Collection(Constants.TestCollections.SuperGroupAdminBots)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class SuperGroupAdminBotTests : IClassFixture<AdminBotTestFixture>
    {
        private readonly AdminBotTestFixture _classFixture;

        private readonly TestsFixture _fixture;

        private ITelegramBotClient BotClient => _fixture.BotClient;

        public SuperGroupAdminBotTests(AdminBotTestFixture classFixture)
        {
            _classFixture = classFixture;
            _fixture = _classFixture.TestsFixture;
            _classFixture.ChatId = _fixture.SuperGroupChatId;
        }

        #region 1. Changing Chat Title

        [Fact(DisplayName = FactTitles.ShouldSetChatTitle)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatTitle)]
        [ExecutionOrder(1)]
        public async Task Should_Set_Chat_Title()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetChatTitle);

            bool result = await BotClient.SetChatTitleAsync(_classFixture.ChatId, _classFixture.ChatTitle);

            Assert.True(result);
        }

        #endregion

        #region 2. Changing Chat Description

        [Fact(DisplayName = FactTitles.ShouldSetChatDescription)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
        [ExecutionOrder(2.1)]
        public async Task Should_Set_Chat_Description()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSetChatDescription);

            bool result = await BotClient.SetChatDescriptionAsync(_classFixture.ChatId,
                "Test Chat Description");

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldDeleteChatDescription)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
        [ExecutionOrder(2.2)]
        public async Task Should_Delete_Chat_Description()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteChatDescription);

            bool result = await BotClient.SetChatDescriptionAsync(_classFixture.ChatId);

            Assert.True(result);
        }

        #endregion

        #region 3. Pinning Chat Description

        [Fact(DisplayName = FactTitles.ShouldPinMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PinChatMessage)]
        [ExecutionOrder(3.1)]
        public async Task Should_Pin_Message()
        {
            Message msg = await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldPinMessage);

            bool result = await BotClient.PinChatMessageAsync(_classFixture.ChatId, msg.MessageId);

            Assert.True(result);
            _classFixture.PinnedMessage = msg;
        }

        [Fact(DisplayName = FactTitles.ShouldGetChatPinnedMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        [ExecutionOrder(3.2)]
        public async Task Should_Get_Chat_Pinned_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetChatPinnedMessage);

            Message pinnedMsg = _classFixture.PinnedMessage;
            if (pinnedMsg is null)
            {
                throw new NullReferenceException("Pinned message should have been set in the preceding test");
            }

            Chat chat = await BotClient.GetChatAsync(_classFixture.ChatId);

            Assert.Equal(pinnedMsg.MessageId, chat.PinnedMessage.MessageId);
            Assert.Equal(pinnedMsg.Text, chat.PinnedMessage.Text);
        }

        [Fact(DisplayName = FactTitles.ShouldUnpinMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
        [ExecutionOrder(3.3)]
        public async Task Should_Unpin_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUnpinMessage);

            bool result = await BotClient.UnpinChatMessageAsync(_classFixture.ChatId);

            Assert.True(result);
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
            bool result;

            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Logo))
            {
                result = await BotClient.SetChatPhotoAsync(_classFixture.ChatId,
                    new FileToSend("photo.png", stream));
            }

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldDeleteChatPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
        [ExecutionOrder(4.2)]
        public async Task Should_Delete_Chat_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteChatPhoto);

            bool result = await BotClient.DeleteChatPhotoAsync(_classFixture.ChatId);

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowOnDeletingChatDeletedPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
        [ExecutionOrder(4.3)]
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

        [Fact(DisplayName = FactTitles.ShouldThrowOnSettingChatStickerSet)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatStickerSet)]
        [ExecutionOrder(5)]
        public async Task Should_Throw_On_Setting_Chat_Sticker_Set()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowOnSettingChatStickerSet);

            const string setName = "EvilMinds";

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                _fixture.BotClient.SetChatStickerSetAsync(_classFixture.ChatId, setName)
            );

            Assert.Equal(400, exception.ErrorCode);
            Assert.Equal("Bad Request: can't set channel sticker set", exception.Message);
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

            public const string ShouldDeleteChatPhoto = "Should delete chat photo";

            public const string ShouldThrowOnDeletingChatDeletedPhoto =
                "Should throw exception in deleting chat photo with no photo currently set";

            public const string ShouldThrowOnSettingChatStickerSet =
                "Should throw exception when trying to set sticker set for a chat with less than 100 members";
        }
    }
}