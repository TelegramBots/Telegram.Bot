using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other
{
    [Collection(Constants.TestCollections.ChatInfo)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ChatInfoTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public ChatInfoTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetSupergroupChat)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        public async Task Should_Get_Supergroup_Chat()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetSupergroupChat);

            Chat supergroupChat = _fixture.SupergroupChat;

            Chat chat = await BotClient.GetChatAsync(
                chatId: supergroupChat.Id
            );

            Assert.Equal(ChatType.Supergroup, chat.Type);
            Assert.Equal(supergroupChat.Id, chat.Id);
            Assert.Equal(supergroupChat.Title, chat.Title);
            Assert.Equal(supergroupChat.Username, chat.Username);
            Assert.Equal(supergroupChat.Description, chat.Description);
            Assert.Equal(supergroupChat.InviteLink, chat.InviteLink);
            Assert.Equal(supergroupChat.PinnedMessage, chat.PinnedMessage);
            Assert.Equal(supergroupChat.StickerSetName, chat.StickerSetName);
            Assert.Equal(supergroupChat.CanSetStickerSet, chat.CanSetStickerSet);
            Assert.Null(chat.FirstName);
            Assert.Null(chat.LastName);
            Assert.False(chat.AllMembersAreAdministrators);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetBotChatMember)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChatMember)]
        public async Task Should_Get_Bot_Chat_Member()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetBotChatMember);

            ChatMember memberBot = await BotClient.GetChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _fixture.BotUser.Id
            );

            Assert.Equal(ChatMemberStatus.Administrator, memberBot.Status);
            Assert.True(memberBot.CanChangeInfo);
            Assert.True(memberBot.CanDeleteMessages);
            Assert.True(memberBot.CanInviteUsers);
            Assert.True(memberBot.CanPromoteMembers);
            Assert.True(memberBot.CanRestrictMembers);
            Assert.True(memberBot.CanPinMessages);
            Assert.False(memberBot.CanBeEdited);
            Assert.Null(memberBot.UntilDate);
            Assert.Null(memberBot.CanPostMessages);
            Assert.Null(memberBot.CanEditMessages);
            Assert.Null(memberBot.CanSendMessages);
            Assert.Null(memberBot.CanSendMediaMessages);
            Assert.Null(memberBot.CanSendOtherMessages);
            Assert.Null(memberBot.CanAddWebPagePreviews);
            Assert.True(JToken.DeepEquals(
                JToken.FromObject(_fixture.BotUser),
                JToken.FromObject(memberBot.User)
            ));
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetSupergroupChatAdmins)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChatAdministrators)]
        public async Task Should_Get_Chat_Admins()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetSupergroupChatAdmins);

            ChatMember[] chatAdmins = await BotClient.GetChatAdministratorsAsync(
                chatId: _fixture.SupergroupChat.Id
            );

            ChatMember memberCreator = Assert.Single(chatAdmins, _ => _.Status == ChatMemberStatus.Creator);
            ChatMember memberBot = Assert.Single(chatAdmins, _ => _.User.IsBot);

            Assert.True(2 <= chatAdmins.Length); // at least, Bot and the Creator
            Assert.Null(memberCreator.UntilDate);
            Assert.Null(memberCreator.CanBeEdited);
            Assert.Null(memberCreator.CanChangeInfo);
            Assert.Null(memberCreator.CanPostMessages);
            Assert.Null(memberCreator.CanEditMessages);
            Assert.Null(memberCreator.CanDeleteMessages);
            Assert.Null(memberCreator.CanInviteUsers);
            Assert.Null(memberCreator.CanRestrictMembers);
            Assert.Null(memberCreator.CanPinMessages);
            Assert.Null(memberCreator.CanPromoteMembers);
            Assert.Null(memberCreator.CanSendMessages);
            Assert.Null(memberCreator.CanSendMediaMessages);
            Assert.Null(memberCreator.CanSendOtherMessages);
            Assert.Null(memberCreator.CanAddWebPagePreviews);
            Assert.True(JToken.DeepEquals(
                JToken.FromObject(_fixture.BotUser),
                JToken.FromObject(memberBot.User)
            ));
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetPrivateChat)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChatAdministrators)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
        public async Task Should_Get_Private_Chat()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetPrivateChat);

            long privateChatId;
            {
                /* In order to have a private chat id, take the Creator of supergroup and use his User ID because
                 * for a regular user, "User ID" is the same number as "Private Chat ID".
                 */
                ChatMember[] chatAdmins = await BotClient.GetChatAdministratorsAsync(_fixture.SupergroupChat);
                privateChatId = chatAdmins
                    .Single(member => member.Status == ChatMemberStatus.Creator)
                    .User.Id;
            }

            Chat chat = await BotClient.GetChatAsync(
                chatId: privateChatId
            );

            Assert.Equal(ChatType.Private, chat.Type);
            Assert.Equal(privateChatId, chat.Id);

            // Mandatory fields:
            Assert.NotEmpty(chat.Username);
            Assert.NotEmpty(chat.FirstName);

            // Following fields of a chat do not apply to a private chat:
            Assert.Null(chat.Title);
            Assert.False(chat.AllMembersAreAdministrators);
            Assert.Null(chat.Description);
            Assert.Null(chat.InviteLink);
            Assert.Null(chat.PinnedMessage);
            Assert.Null(chat.StickerSetName);
            Assert.Null(chat.CanSetStickerSet);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetChatMembersCount)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChatMembersCount)]
        public async Task Should_Get_Chat_Members_Count()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetChatMembersCount);

            int membersCount = await BotClient.GetChatMembersCountAsync(
                chatId: _fixture.SupergroupChat.Id
            );

            Assert.True(2 <= membersCount); // at least, Bot and the Creator
        }

        /// <remarks>
        /// The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear
        /// its typing status)
        /// </remarks>
        [OrderedFact(DisplayName = FactTitles.ShouldSendChatAction)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendChatAction)]
        public async Task Should_Send_Chat_Action()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendChatAction);

            await BotClient.SendChatActionAsync(
                chatId: _fixture.SupergroupChat.Id,
                chatAction: ChatAction.RecordAudio
            );

            await Task.Delay(5_000);
        }

        private static class FactTitles
        {
            public const string ShouldGetPrivateChat = "Should get private chat info";

            public const string ShouldGetSupergroupChat = "Should get supergroup chat info";

            public const string ShouldGetBotChatMember = "Should get chat member: bot(admin)";

            public const string ShouldGetSupergroupChatAdmins = "Should get supergroup chat administrators";

            public const string ShouldGetChatMembersCount = "Should get chat members count";

            public const string ShouldSendChatAction = "Should send action to chat: recording audio";
        }
    }
}
