using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.AdminBots
{
    [Collection(CommonConstants.TestCollections.ChatMemberAdministration)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class ChatMemberAdministrationTests : IClassFixture<ChatAdministrationFixture>
    {
        private readonly BotClientFixture _fixture;

        private readonly ChatAdministrationFixture _classFixture;

        public ChatMemberAdministrationTests(ChatAdministrationFixture classFixture)
        {
            _classFixture = classFixture;
            _fixture = classFixture.AssemblyFixture;
        }

        #region 1. Kick, Unban, and Invite chat member back

        [Fact(DisplayName = FactTitles.ShouldKickChatMemberForEver)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.KickChatMember)]
        [ExecutionOrder(1.1)]
        public async Task ShouldKickChatMemberForEver()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldKickChatMemberForEver);

            bool result = await _fixture.BotClient.KickChatMemberAsync(_fixture.SuperGroupChatId,
                _classFixture.RegularMemberId);

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldUnbanChatMember)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.UnbanChatMember)]
        [ExecutionOrder(1.2)]
        public async Task ShouldUnbanChatMember()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldUnbanChatMember);

            bool result = await _fixture.BotClient.UnbanChatMemberAsync(_fixture.SuperGroupChatId,
                _classFixture.RegularMemberId);

            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldExportChatInviteLink)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.ExportChatInviteLink)]
        [ExecutionOrder(1.3)]
        public async Task ShouldExportChatInviteLink()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldExportChatInviteLink);

            string result = await _fixture.BotClient.ExportChatInviteLinkAsync(_fixture.SuperGroupChatId);

            Assert.StartsWith("https://t.me/joinchat/", result);

            _classFixture.GroupInviteLink = result;
        }

        [Fact(DisplayName = FactTitles.ShouldReceiveNewChatMemberNotification)]
        [ExecutionOrder(1.4)]
        public async Task ShouldReceiveNewChatMemberNotification()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldReceiveNewChatMemberNotification,
                $"{_classFixture.RegularMemberName} should join the group using invite link sent to " +
                "him/her in private chat");

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

            await _fixture.BotClient.SendTextMessageAsync(_classFixture.RegularMemberPrivateChatId,
                _classFixture.GroupInviteLink);

            Update update = (await _fixture.UpdateReceiver.GetUpdatesAsync(u =>
                    u.Message.Chat.Type == ChatType.Supergroup &&
                    u.Message.Chat.Id.ToString() == _fixture.SuperGroupChatId.ToString() &&
                    u.Message.Type == MessageType.ServiceMessage,
                updateTypes: UpdateType.MessageUpdate)).Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

            Message serviceMsg = update.Message;

            Assert.Equal(_classFixture.RegularMemberId.ToString(), serviceMsg.NewChatMember.Id.ToString());
            Assert.Equal(_classFixture.RegularMemberId.ToString(), serviceMsg.NewChatMembers.Single().Id.ToString());
        }

        #endregion

        #region 2. Restrict, and Promote Chat Member

        // todo

        #endregion

        #region 3. Kick chat member temporarily

        [Fact(DisplayName = FactTitles.ShouldKickChatMemberTemporarily)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.KickChatMember)]
        [ExecutionOrder(3)]
        public async Task ShouldKickChatMemberTemporarily()
        {
            const int banSeconds = 35;
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldKickChatMemberTemporarily,
                $"{_classFixture.RegularMemberName} should be able to join again in *{banSeconds} seconds* " +
                "via the link shared in private chat with him/her");

            bool result = await _fixture.BotClient.KickChatMemberAsync(_fixture.SuperGroupChatId,
                _classFixture.RegularMemberId, DateTime.UtcNow.AddSeconds(banSeconds));

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
        }
    }
}
