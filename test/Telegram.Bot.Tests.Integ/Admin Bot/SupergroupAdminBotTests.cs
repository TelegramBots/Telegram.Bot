using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Message msg1 = await _classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This message will be deleted second");
            Message msg2 = await _classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This will be deleted as group");
            Message msg3 = await _classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This will be deleted with previous one");
            Message msg4 = await _classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This message will be deleted first");

            await BotClient.PinChatMessageAsync(
                chatId: _classFixture.Chat.Id,
                messageId: msg1.MessageId,
                disableNotification: true
            );

            await BotClient.PinChatMessageAsync(
                chatId: _classFixture.Chat.Id,
                messageId: msg2.MessageId,
                disableNotification: true
            );

            await BotClient.PinChatMessageAsync(
                chatId: _classFixture.Chat.Id,
                messageId: msg3.MessageId,
                disableNotification: true
            );

            await BotClient.PinChatMessageAsync(
                chatId: _classFixture.Chat.Id,
                messageId: msg4.MessageId,
                disableNotification: true
            );
            _classFixture.PinnedMessages.Add(msg1);
            _classFixture.PinnedMessages.Add(msg2);
            _classFixture.PinnedMessages.Add(msg3);
            _classFixture.PinnedMessages.Add(msg4);
        }

        [OrderedFact("Should get chat's pinned message")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        public async Task Should_Get_Last_Chat_Pinned_Message()
        {
            Message pinnedMsg = _classFixture.PinnedMessages.Last();

            Chat chat = await BotClient.GetChatAsync(_classFixture.Chat.Id);

            Assert.True(JToken.DeepEquals(
                JToken.FromObject(pinnedMsg), JToken.FromObject(chat.PinnedMessage)
            ));
        }

        [OrderedFact("Should unpin last chat message")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
        public async Task Should_Unpin_Last_Message()
        {
            await BotClient.UnpinChatMessageAsync(_classFixture.Chat.Id);

            Chat chat = await BotClient.GetChatAsync(_classFixture.Chat.Id);

            Assert.False(JToken.DeepEquals(
                JToken.FromObject(_classFixture.PinnedMessages.Last()), JToken.FromObject(chat.PinnedMessage)
            ));
        }

        [OrderedFact("Should unpin first chat message")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
        public async Task Should_Unpin_First_Message()
        {
            await BotClient.UnpinChatMessageAsync(_classFixture.Chat.Id, messageId: _classFixture.PinnedMessages.First().MessageId);
        }

        [OrderedFact("Should Unpin all Messages")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinAllChatMessages)]
        public async Task Should_Unpin_All_Messages()
        {
            await BotClient.UnpinAllChatMessages(_classFixture.Chat);
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
