using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using Constants = Telegram.Bot.Tests.Integ.Framework.Constants;

namespace Telegram.Bot.Tests.Integ.Admin_Bot
{
    [Collection(Constants.TestCollections.ChatMemberAdministration)]
    [Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
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

        #region Kick, Unban, and Invite chat member back

        [OrderedFact("Should kick user from chat and ban him/her for ever")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.KickChatMember)]
        public async Task Should_Kick_Chat_Member_For_Ever()
        {
            await BotClient.KickChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId
            );
        }

        [OrderedFact("Should unban a chat member")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnbanChatMember)]
        public async Task Should_Unban_Chat_Member()
        {
            await BotClient.UnbanChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId
            );
        }

        [OrderedFact("Should export an invite link to the group")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.ExportChatInviteLink)]
        public async Task Should_Export_Chat_Invite_Link()
        {
            string result = await BotClient.ExportChatInviteLinkAsync(_fixture.SupergroupChat.Id);

            Assert.StartsWith("https://t.me/joinchat/", result);

            _classFixture.GroupInviteLink = result;
        }

        [OrderedFact("Should receive a notification of new member (same kicked member) joining the chat")]
        public async Task Should_Receive_New_Chat_Member_Notification()
        {
            await _fixture.SendTestInstructionsAsync(
                $"@{_classFixture.RegularMemberUserName.Replace("_", @"\_")} should join the group using invite link sent to " +
                "him/her in private chat"
            );

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

            await BotClient.SendTextMessageAsync(
                chatId: _classFixture.RegularMemberChat,
                text: _classFixture.GroupInviteLink
            );

            Update update = (await _fixture.UpdateReceiver
                    .GetUpdatesAsync(u =>
                            u.Message.Chat.Type == ChatType.Supergroup &&
                            u.Message.Chat.Id.ToString() == _fixture.SupergroupChat.Id.ToString() &&
                            u.Message.Type == MessageType.ChatMembersAdded,
                        updateTypes: UpdateType.Message)
                ).Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

            Message serviceMsg = update.Message;

            Assert.Equal(_classFixture.RegularMemberUserId.ToString(),
                serviceMsg.NewChatMembers.Single().Id.ToString());
        }

        #endregion

        #region Promote and Restrict Chat Member

        [OrderedFact("Should promote chat member to change chat information")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PromoteChatMember)]
        public async Task Should_Promote_User_To_Change_Chat_Info()
        {
            //ToDo exception when user isn't in group. Bad Request: bots can't add new chat members

            await BotClient.PromoteChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId,
                canChangeInfo: true
            );
        }

        [OrderedFact("Should set a custom title for the previously promoted admin")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatAdministratorCustomTitle)]
        public async Task Should_Set_Custom_Title_For_Admin()
        {
            ChatMember promotedRegularUser = await BotClient.GetChatMemberAsync(
                _fixture.SupergroupChat,
                _classFixture.RegularMemberUserId
            );

            await BotClient.SetChatAdministratorCustomTitleAsync(
                chatId: _fixture.SupergroupChat,
                userId: promotedRegularUser.User.Id,
                customTitle: "CHANGED TITLE"
            );

            ChatMember newChatMember = await BotClient.GetChatMemberAsync(
                _fixture.SupergroupChat,
                promotedRegularUser.User.Id
            );

            Assert.Equal("CHANGED TITLE", newChatMember.CustomTitle);

            // Restore default title by sending empty string
            await BotClient.SetChatAdministratorCustomTitleAsync(
                chatId: _fixture.SupergroupChat,
                userId: promotedRegularUser.User.Id,
                customTitle: string.Empty
            );
        }

        [OrderedFact("Should demote chat member by taking his/her only admin right: change_info")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PromoteChatMember)]
        public async Task Should_Demote_User()
        {
            //ToDo exception when user isn't in group. Bad Request: USER_NOT_MUTUAL_CONTACT

            await BotClient.PromoteChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId,
                canChangeInfo: false
            );
        }

        [OrderedFact("Should restrict chat member from sending stickers temporarily")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.RestrictChatMember)]
        public async Task Should_Restrict_Sending_Stickers_Temporarily()
        {
            const int banSeconds = 35;

            await BotClient.RestrictChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId,
                untilDate: DateTime.UtcNow.AddSeconds(banSeconds),
                permissions: new ChatPermissions
                {
                    CanSendMessages = true,
                    CanSendMediaMessages = true,
                    CanSendOtherMessages = false
                }
            );
        }

        #endregion

        #region Receving chat member status update

        [OrderedFact("Should receive chat member updated")]
        public async Task Should_Receive_Chat_Member_Updated()
        {
            await _fixture.SendTestInstructionsAsync(
                $"Chat admin should kick @{_classFixture.RegularMemberUserName.Replace("_", @"\_")}."
            );

            Update update = (await _fixture.UpdateReceiver
                    .GetUpdatesAsync(
                        u => u.ChatMember?.Chat.Id == _fixture.SupergroupChat.Id,
                        updateTypes: new [] { UpdateType.ChatMember }
                    ).ConfigureAwait(false)
                ).Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

            ChatMemberUpdated chatMemberUpdated = update.ChatMember;

            Assert.True(chatMemberUpdated.NewChatMember.Status == ChatMemberStatus.Kicked);
            Assert.Equal(_classFixture.RegularMemberUserId, chatMemberUpdated.NewChatMember.User.Id);
        }

        // This section is needed for technical reasons, don't remove
        [OrderedFact("Should_Wait_For_Regular_Chat_Member_To_Join")]
        public async Task Should_Wait_For_Regular_Chat_Member_To_Join()
        {
            TimeSpan waitTime = TimeSpan.FromMinutes(2);
            using CancellationTokenSource cts = new(TimeSpan.FromMinutes(2));

            await _fixture.SendTestInstructionsAsync(
                $"Chat admin should invite @{_classFixture.RegularMemberUserName.Replace("_", @"\_")} back to the group. Bot will be waiting" +
                $" for {waitTime.Minutes} minutes."
            );

            Update _ = (await _fixture.UpdateReceiver
                    .GetUpdatesAsync(
                        u => u.Message.Chat.Id == _fixture.SupergroupChat.Id &&
                             u.Message.Type == MessageType.ChatMembersAdded,
                        updateTypes: new[] {UpdateType.Message},
                        cancellationToken: cts.Token
                    )
                ).Single();

            // ReSharper disable once MethodSupportsCancellation
            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();
        }

        #endregion

        #region Kick chat member temporarily

        [OrderedFact("Should kick user from chat and ban him/her temporarily")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.KickChatMember)]
        public async Task Should_Kick_Chat_Member_Temporarily()
        {
            const int banSeconds = 35;
            await _fixture.SendTestInstructionsAsync(
                $"@{_classFixture.RegularMemberUserName.Replace("_", @"\_")} should be able to join again in" +
                $" *{banSeconds} seconds* via the link shared in private chat with him/her"
            );

            await BotClient.KickChatMemberAsync(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId,
                untilDate: DateTime.UtcNow.AddSeconds(banSeconds)
            );
        }

        #endregion
    }
}
