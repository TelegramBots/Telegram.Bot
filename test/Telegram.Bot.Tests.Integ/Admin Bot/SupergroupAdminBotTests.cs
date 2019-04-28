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
    public class SupergroupAdminBotTests : IClassFixture<AdminBotTestFixture>
    {
        private readonly AdminBotTestFixture _classFixture;

        private readonly TestsFixture _fixture;

        private ITelegramBotClient BotClient => _fixture.BotClient;

        public SupergroupAdminBotTests(TestsFixture testsFixture, AdminBotTestFixture classFixture)
        {
            _fixture = testsFixture;
            _classFixture = classFixture;
            _classFixture.Chat = _fixture.SupergroupChat;
        }

        #region 1. Changing Chat Title

        [OrderedFact("Should set chat title")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatTitle)]
        public async Task Should_Set_Chat_Title()
        {
            await BotClient.SetChatTitleAsync(
                chatId: _classFixture.Chat.Id,
                title: _classFixture.ChatTitle
            );
        }

        #endregion

        #region 2. Changing Chat Description

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

        #region 3. Pinning Chat Description

        [OrderedFact("Should pin chat message")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PinChatMessage)]
        public async Task Should_Pin_Message()
        {
            Message msg = await _fixture.SendTestInstructionsAsync("🧷 This message will be pinned shortly!");

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

        #region 4. Changing Chat Photo

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

        /// <summary>
        /// If chat had a photo before, reset the photo back.
        /// </summary>
        [OrderedFact("Should reset the same old chat photo if existed")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatPhoto)]
        public async Task Should_Reset_Old_Chat_Photo_If_Existed()
        {
            // "Chat.Photo" might be null if there is no photo currently set
            string previousChatPhotoId = _classFixture.Chat.Photo?.BigFileId;
            if (previousChatPhotoId == default)
            {
                // chat didn't have a photo
                return;
            }

            using (Stream photoStream = new MemoryStream())
            {
                // pass photo's file_id, prepare file for download, and download the file into memory
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

        [OrderedFact("Should throw exception when trying to set sticker set for a chat with less than 100 members")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatStickerSet)]
        public async Task Should_Throw_On_Setting_Chat_Sticker_Set()
        {
            const string setName = "EvilMinds";

            ApiRequestException exception = await Assert.ThrowsAnyAsync<ApiRequestException>(() =>
                _fixture.BotClient.SetChatStickerSetAsync(_classFixture.Chat.Id, setName)
            );

            // ToDo: Create exception type
            Assert.Equal(400, exception.ErrorCode);
            Assert.Equal("Bad Request: can't set supergroup sticker set", exception.Message);
        }

        #endregion
    }
}
