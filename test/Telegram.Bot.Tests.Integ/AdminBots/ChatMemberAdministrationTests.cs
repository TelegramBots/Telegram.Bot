using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.AdminBots
{
    [Collection(Constants.TestCollections.ChatMemberAdministration)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ChatMemberAdministrationTests : IClassFixture<ChatMemberAdministrationTestFixture>
    {
        private readonly TestsFixture _fixture;

        private readonly ChatMemberAdministrationTestFixture _classFixture;

        private ITelegramBotClient BotClient => _fixture.BotClient;

        public ChatMemberAdministrationTests(ChatMemberAdministrationTestFixture classFixture)
        {
            _classFixture = classFixture;
            _fixture = classFixture.TestsFixture;
        }

        #region 1. Kick, Unban, and Invite chat member back

        [Fact(DisplayName = FactTitles.ShouldKickChatMemberForEver)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.KickChatMember)]
        [ExecutionOrder(1.1)]
        public async Task Should_Kick_Chat_Member_For_Ever()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldKickChatMemberForEver);

            bool result = await BotClient.KickChatMemberAsync(_fixture.SuperGroupChatId,
                _classFixture.RegularMemberUserId);

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldUnbanChatMember)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnbanChatMember)]
        [ExecutionOrder(1.2)]
        public async Task Should_Unban_Chat_Member()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUnbanChatMember);

            bool result = await BotClient.UnbanChatMemberAsync(_fixture.SuperGroupChatId,
                _classFixture.RegularMemberUserId);

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldExportChatInviteLink)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.ExportChatInviteLink)]
        [ExecutionOrder(1.3)]
        public async Task Should_Export_Chat_Invite_Link()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldExportChatInviteLink);

            string result = await BotClient.ExportChatInviteLinkAsync(_fixture.SuperGroupChatId);

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

            await BotClient.SendTextMessageAsync(_classFixture.RegularMemberPrivateChatId,
                _classFixture.GroupInviteLink);

            Update update = (await _fixture.UpdateReceiver.GetUpdatesAsync(u =>
                    u.Message.Chat.Type == ChatType.Supergroup &&
                    u.Message.Chat.Id.ToString() == _fixture.SuperGroupChatId.ToString() &&
                    u.Message.Type == MessageType.ServiceMessage,
                updateTypes: UpdateType.MessageUpdate)).Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

            Message serviceMsg = update.Message;

            Assert.Equal(_classFixture.RegularMemberUserId.ToString(), serviceMsg.NewChatMember.Id.ToString());
            Assert.Equal(_classFixture.RegularMemberUserId.ToString(), serviceMsg.NewChatMembers.Single().Id.ToString());
        }

        #endregion

        #region 2. Promote and Restrict Chat Member

        [Fact(DisplayName = FactTitles.ShouldPromoteUserToChangeChatInfo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PromoteChatMember)]
        [ExecutionOrder(2.1)]
        public async Task Should_Promote_User_To_Change_Chat_Info()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldPromoteUserToChangeChatInfo);

            bool result = await BotClient.PromoteChatMemberAsync(
                _fixture.SuperGroupChatId,
                _classFixture.RegularMemberUserId,
                canChangeInfo: false);

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldRestrictSendingStickersTemporarily)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.RestrictChatMember)]
        [ExecutionOrder(2.2)]
        public async Task Should_Restrict_Sending_Stickers_Temporarily()
        {
            const int banSeconds = 35;
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldRestrictSendingStickersTemporarily);

            bool result = await BotClient.RestrictChatMemberAsync(
                _fixture.SuperGroupChatId,
                _classFixture.RegularMemberUserId,
                DateTime.UtcNow.AddSeconds(banSeconds),
                canSendMessages: true,
                canSendMediaMessages: true,
                canSendOtherMessages: false);

            Assert.True(result);
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
                $"@{_classFixture.RegularMemberUserName.Replace("_", @"\_")} should be able to join again in *{banSeconds} seconds* " +
                "via the link shared in private chat with him/her");

            bool result = await BotClient.KickChatMemberAsync(_fixture.SuperGroupChatId,
                _classFixture.RegularMemberUserId, DateTime.UtcNow.AddSeconds(banSeconds));

            Assert.True(result);
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

            public const string ShouldRestrictSendingStickersTemporarily = "Should restrict chat member from sending stickers temporarily";
        }
    }
}
