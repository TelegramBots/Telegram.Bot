using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Admin_Bot
{
    [Collection(Constants.TestCollections.ChatMemberAdministration)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ChatMemberAdministrationTests : IClassFixture<ChatMemberAdministrationTestFixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly ChatMemberAdministrationTestFixture _classFixture;

        public ChatMemberAdministrationTests(TestsFixture fixture, ChatMemberAdministrationTestFixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        #region 1. Kick, Unban, and Invite chat member back

        [Fact(DisplayName = FactTitles.ShouldKickChatMemberForEver)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.KickChatMember)]
        [ExecutionOrder(1.1)]
        public async Task Should_Kick_Chat_Member_For_Ever()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldKickChatMemberForEver);

            await BotClient.KickChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId
            );
        }

        [Fact(DisplayName = FactTitles.ShouldUnbanChatMember)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnbanChatMember)]
        [ExecutionOrder(1.2)]
        public async Task Should_Unban_Chat_Member()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUnbanChatMember);

            await BotClient.UnbanChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId
            );
        }

        [Fact(DisplayName = FactTitles.ShouldExportChatInviteLink)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.ExportChatInviteLink)]
        [ExecutionOrder(1.3)]
        public async Task Should_Export_Chat_Invite_Link()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldExportChatInviteLink);

            string result = await BotClient.ExportChatInviteLinkAsync(_fixture.SupergroupChat.Id);

            Assert.StartsWith("https://t.me/joinchat/", result);

            _classFixture.GroupInviteLink = result;
        }

        [Fact(DisplayName = FactTitles.ShouldReceiveNewChatMemberNotification)]
        [ExecutionOrder(1.4)]
        public async Task Should_Receive_New_Chat_Member_Notification()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldReceiveNewChatMemberNotification,
                $"@{_classFixture.RegularMemberUserName.Replace("_", @"\_")} should join the group using invite link sent to " +
                "him/her in private chat");

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

            await BotClient.SendTextMessageAsync(
                chatId: _classFixture.RegularMemberChat,
                text: _classFixture.GroupInviteLink
            );

            Update update = (await _fixture.UpdateReceiver
                .GetUpdatesAsync(u =>
                    u.Message.Chat.Type == ChatType.Supergroup &&
                    u.Message.Chat.Id.ToString() == _fixture.SupergroupChat.Id.ToString() &&
                    u.Message.Type == MessageType.Service,
                updateTypes: UpdateType.Message)
            ).Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

            Message serviceMsg = update.Message;

            Assert.Equal(_classFixture.RegularMemberUserId.ToString(), serviceMsg.NewChatMembers.Single().Id.ToString());
        }

        #endregion

        #region 2. Promote and Restrict Chat Member

        [Fact(DisplayName = FactTitles.ShouldPromoteUserToChangeChatInfo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PromoteChatMember)]
        [ExecutionOrder(2.1)]
        public async Task Should_Promote_User_To_Change_Chat_Info()
        {
            //ToDo exception when user isn't in group. Bad Request: bots can't add new chat members

            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldPromoteUserToChangeChatInfo);

            await BotClient.PromoteChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId,
                canChangeInfo: true
            );
        }

        [Fact(DisplayName = FactTitles.ShouldDemoteUser)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PromoteChatMember)]
        [ExecutionOrder(2.2)]
        public async Task Should_Demote_User()
        {
            //ToDo exception when user isn't in group. Bad Request: USER_NOT_MUTUAL_CONTACT

            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDemoteUser);

            await BotClient.PromoteChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId,
                canChangeInfo: false
            );
        }

        [Fact(DisplayName = FactTitles.ShouldRestrictSendingStickersTemporarily)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.RestrictChatMember)]
        [ExecutionOrder(2.3)]
        public async Task Should_Restrict_Sending_Stickers_Temporarily()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldRestrictSendingStickersTemporarily);
            const int banSeconds = 35;

            await BotClient.RestrictChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId,
                untilDate: DateTime.UtcNow.AddSeconds(banSeconds),
                canSendMessages: true,
                canSendMediaMessages: true,
                canSendOtherMessages: false
            );
        }

        #endregion

        #region 3. Kick chat member temporarily

        [Fact(DisplayName = FactTitles.ShouldKickChatMemberTemporarily)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.KickChatMember)]
        [ExecutionOrder(3)]
        public async Task Should_Kick_Chat_Member_Temporarily()
        {
            const int banSeconds = 35;
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldKickChatMemberTemporarily,
                $"@{_classFixture.RegularMemberUserName.Replace("_", @"\_")} should be able to join again in" +
                $" *{banSeconds} seconds* via the link shared in private chat with him/her");

            await BotClient.KickChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId,
                untilDate: DateTime.UtcNow.AddSeconds(banSeconds)
            );
        }

        #endregion

        private static class FactTitles
        {
            public const string ShouldKickChatMemberForEver = "Should kick user from chat and ban him/her for ever";

            public const string ShouldUnbanChatMember = "Should unban a chat member";

            public const string ShouldExportChatInviteLink = "Should export an invite link to the group";

            public const string ShouldReceiveNewChatMemberNotification =
                "Should receive a notification of new member (same kicked member) joining the chat";

            public const string ShouldKickChatMemberTemporarily = "Should kick user from chat and ban him/her temporarily";

            public const string ShouldPromoteUserToChangeChatInfo = "Should promote chat member to change chat information";

            public const string ShouldDemoteUser = "Should demote chat member by taking his/her only admin right: change_info";

            public const string ShouldRestrictSendingStickersTemporarily = "Should restrict chat member from sending stickers temporarily";
        }
    }
}
