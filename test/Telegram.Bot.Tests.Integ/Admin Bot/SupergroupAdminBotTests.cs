using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Admin_Bot
{
    [Collection(Constants.TestCollections.SupergroupAdminBots)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class SupergroupAdminBotTests : IClassFixture<SupergroupAdminBotTestsFixture>
    {
        private readonly SupergroupAdminBotTestsFixture _classFixture;

        private ITelegramBotClient BotClient => _classFixture.TestsFixture.BotClient;

        public SupergroupAdminBotTests(SupergroupAdminBotTestsFixture classFixture)
        {
            _classFixture = classFixture;
        }

        #region 1. Changing Chat Title

        [OrderedFact("Should set chat title")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatTitle)]
        public async Task Should_Set_Chat_Title()
        {
            await BotClient.SetChatTitleAsync(
                chatId: _classFixture.Chat.Id,
                title: "Test Chat Title"
            );
        }

        #endregion

        #region 2. Changing Chat default permissions

        [OrderedFact("Should set new default permissions")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatPermissions)]
        public async Task Should_Set_New_Default_Permissions()
        {
            ChatPermissions newDefaultPermissions = new ChatPermissions()
            {
                CanInviteUsers = false,
                CanSendMediaMessages = true,
                CanChangeInfo = false,
                CanSendMessages = true,
                CanPinMessages = false,
                CanSendPolls = false,
                CanSendOtherMessages = false,
                CanAddWebPagePreviews = false
            };

            await BotClient.SetChatPermissionsAsync(_classFixture.Chat.Id, newDefaultPermissions);
            Chat supergroup = await BotClient.GetChatAsync(_classFixture.Chat.Id);
            ChatPermissions setChatPermissions = supergroup.Permissions;

            Asserts.JsonEquals(newDefaultPermissions, setChatPermissions);
        }

        #endregion

        #region 3. Changing Chat Description

        [OrderedFact("Should set chat description")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
        public async Task Should_Set_Chat_Description()
        {
            await BotClient.SetChatDescriptionAsync(
                chatId: _classFixture.Chat.Id,
                description: "Test Chat Description"
            );
        }

        [OrderedFact("Should delete chat description")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
        public async Task Should_Delete_Chat_Description()
        {
            // ToDo: exception Bad Request: chat description is not modified

            await BotClient.SetChatDescriptionAsync(
                chatId: _classFixture.Chat.Id
            );
        }

        #endregion

        #region 4. Pinning Chat Description

        [OrderedFact("Should pin chat message")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PinChatMessage)]
        public async Task Should_Pin_Message()
        {
            Message msg = await _classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This message will be pinned shortly!");

            await BotClient.PinChatMessageAsync(
                chatId: _classFixture.Chat.Id,
                messageId: msg.MessageId,
                disableNotification: true
            );

            _classFixture.PinnedMessage = msg;
        }

        [OrderedFact("Should get chat's pinned message")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        public async Task Should_Get_Chat_Pinned_Message()
        {
            Message pinnedMsg = _classFixture.PinnedMessage;

            Chat chat = await BotClient.GetChatAsync(_classFixture.Chat.Id);

            Assert.True(JToken.DeepEquals(
                JToken.FromObject(pinnedMsg), JToken.FromObject(chat.PinnedMessage)
            ));
        }

        [OrderedFact("Should unpin chat message")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
        public async Task Should_Unpin_Message()
        {
            await BotClient.UnpinChatMessageAsync(_classFixture.Chat.Id);
        }

        [OrderedFact("Should get the chat info without a pinned message")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        public async Task Should_Get_Chat_With_No_Pinned_Message()
        {
            Chat chat = await BotClient.GetChatAsync(_classFixture.Chat.Id);

            Assert.Null(chat.PinnedMessage);
        }

        #endregion

        #region 5. Changing Chat Photo

        [OrderedFact("Should set chat photo")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatPhoto)]
        public async Task Should_Set_Chat_Photo()
        {
            using (Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Logo))
            {
                await BotClient.SetChatPhotoAsync(
                    chatId: _classFixture.Chat.Id,
                    photo: stream
                );
            }
        }

        [OrderedFact("Should delete chat photo")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
        public async Task Should_Delete_Chat_Photo()
        {
            await BotClient.DeleteChatPhotoAsync(_classFixture.Chat.Id);
        }

        [OrderedFact("Should throw exception in deleting chat photo with no photo currently set")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
        public async Task Should_Throw_On_Deleting_Chat_Deleted_Photo()
        {
            Exception e = await Assert.ThrowsAnyAsync<Exception>(() =>
                BotClient.DeleteChatPhotoAsync(_classFixture.Chat.Id));

            // ToDo: Create exception type
            Assert.IsType<ApiRequestException>(e);
            Assert.Equal("Bad Request: CHAT_NOT_MODIFIED", e.Message);
        }

        #endregion

        #region 6. Chat Sticker Set

        [OrderedFact("Should throw exception when trying to set sticker set for a chat with less than 100 members")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatStickerSet)]
        public async Task Should_Throw_On_Setting_Chat_Sticker_Set()
        {
            const string setName = "EvilMinds";

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                BotClient.SetChatStickerSetAsync(_classFixture.Chat.Id, setName)
            );

            // ToDo: Create exception type
            Assert.Equal(400, exception.ErrorCode);
            Assert.Equal("Bad Request: can't set supergroup sticker set", exception.Message);
        }

        #endregion
    }
}
