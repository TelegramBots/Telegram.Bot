using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;
using Constants = Telegram.Bot.Tests.Integ.Framework.Constants;

namespace Telegram.Bot.Tests.Integ.Admin_Bot;

[Collection(Constants.TestCollections.ChatMemberAdministration)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class ChatMemberAdministrationTests : IClassFixture<ChatMemberAdministrationTestFixture>
{
    ITelegramBotClient BotClient => _fixture.BotClient;

    readonly TestsFixture _fixture;

    readonly ChatMemberAdministrationTestFixture _classFixture;

    public ChatMemberAdministrationTests(TestsFixture fixture, ChatMemberAdministrationTestFixture classFixture)
    {
        _fixture = fixture;
        _classFixture = classFixture;
    }

    #region Kick, Unban, and Invite chat member back

    [OrderedFact("Should get regular chat member with member status and of ChatMemberMember type")]
    public async Task Should_Get_Chat_Member_Member()
    {
        ChatMember chatMember = await BotClient.GetChatMemberAsync(
            chatId: _fixture.SupergroupChat,
            userId: _classFixture.RegularMemberUserId
        );

        Assert.Equal(ChatMemberStatus.Member, chatMember.Status);
        Assert.IsType<ChatMemberMember>(chatMember);
    }

    [OrderedFact("Should kick user from chat and ban them for ever")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.KickChatMember)]
    public async Task Should_Kick_Chat_Member_For_Ever()
    {
        await BotClient.BanChatMemberAsync(
            chatId: _fixture.SupergroupChat.Id,
            userId: _classFixture.RegularMemberUserId
        );
    }

    [OrderedFact("Should get banned chat member with kicked status and of ChatMemberBanned type")]
    public async Task Should_Get_Chat_Member_Kicked()
    {
        ChatMember chatMember = await BotClient.GetChatMemberAsync(
            chatId: _fixture.SupergroupChat,
            userId: _classFixture.RegularMemberUserId
        );

        Assert.Equal(ChatMemberStatus.Kicked, chatMember.Status);
        ChatMemberBanned bannedChatMember = Assert.IsType<ChatMemberBanned>(chatMember);
        Assert.Equal(default, bannedChatMember.UntilDate);
        Assert.Null(bannedChatMember.UntilDate);
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
        string chatInviteLink = await BotClient.ExportChatInviteLinkAsync(_fixture.SupergroupChat.Id);

        Assert.Matches("https://t.me/.+", chatInviteLink);

        _classFixture.GroupInviteLink = chatInviteLink;
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

        Update update = await _fixture.UpdateReceiver.GetUpdateAsync(
            predicate: u =>
                u.Message!.Chat.Type == ChatType.Supergroup
                && u.Message!.Chat.Id == _fixture.SupergroupChat.Id
                && u.Message!.Type == MessageType.ChatMembersAdded,
            updateTypes: new[] { UpdateType.Message }
        );

        await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

        Message serviceMsg = update.Message;

        Assert.NotNull(serviceMsg);
        Assert.NotNull(serviceMsg.NewChatMembers);
        Assert.NotEmpty(serviceMsg.NewChatMembers);
        User newUser = Assert.Single(serviceMsg.NewChatMembers);

        Assert.Equal(
            _classFixture.RegularMemberUserId.ToString(),
            newUser!.Id.ToString()
        );
    }

    [OrderedFact("Should create an invite link to the group")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateChatInviteLink)]
    public async Task Should_Create_Chat_Invite_Link()
    {
        DateTime createdAt = DateTime.UtcNow;

        // Milliseconds are ignored during conversion to unix timestamp since it counts only up to
        // seconds, so for equality to work later on assertion we need to zero out milliseconds
        DateTime expireDate = createdAt.With(new () {Millisecond = 0}).AddHours(1);

        string inviteLinkName = $"Created at {createdAt:yyyy-MM-ddTHH:mm:ss}";

        ChatInviteLink chatInviteLink = await BotClient.CreateChatInviteLinkAsync(
            chatId: _fixture.SupergroupChat.Id,
            createsJoinRequest: true,
            name: inviteLinkName,
            expireDate: expireDate
        );

        Assert.NotNull(chatInviteLink);
        Assert.NotNull(chatInviteLink.Creator);
        Assert.NotNull(chatInviteLink.InviteLink);
        Assert.Matches("https://t.me/.+", chatInviteLink.InviteLink);
        Assert.False(chatInviteLink.IsRevoked);
        Assert.False(chatInviteLink.IsPrimary);
        Assert.Null(chatInviteLink.MemberLimit);
        Assert.True(chatInviteLink.CreatesJoinRequest);
        Assert.Null(chatInviteLink.PendingJoinRequestCount);
        Assert.Equal(inviteLinkName, chatInviteLink.Name);
        Assert.Equal(expireDate, chatInviteLink.ExpireDate);

        _classFixture.ChatInviteLink = chatInviteLink;
    }

    [OrderedFact("Should receive a notification of new member (same kicked member) joining the chat")]
    public async Task Should_Receive_Chat_Join_Request()
    {
        await _fixture.SendTestInstructionsAsync(
            $"@{_classFixture.RegularMemberUserName.Replace("_", @"\_")} should send a request to join the" +
            "chat by following the invite link sent to them in private chat two time. The administrator should " +
            "decline the first attempt and then approve the second one."
        );

        await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

        await BotClient.BanChatMemberAsync(
            chatId: _fixture.SupergroupChat.Id,
            userId: _classFixture.RegularMemberUserId
        );

        await Task.Delay(TimeSpan.FromSeconds(3));

        await BotClient.UnbanChatMemberAsync(
            chatId: _fixture.SupergroupChat.Id,
            userId: _classFixture.RegularMemberUserId
        );

        await Task.Delay(TimeSpan.FromSeconds(3));

        await BotClient.SendTextMessageAsync(
            chatId: _classFixture.RegularMemberChat,
            text: _classFixture.ChatInviteLink.InviteLink
        );

        await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

        Update update = await _fixture.UpdateReceiver.GetUpdateAsync(
            predicate: u => u.ChatJoinRequest is not null
        );

        ChatJoinRequest chatJoinRequest = update.ChatJoinRequest;

        Assert.NotNull(chatJoinRequest);
        Assert.NotNull(chatJoinRequest.InviteLink);
        Assert.NotNull(chatJoinRequest.Chat);
        Assert.NotNull(chatJoinRequest.From);
        Assert.NotEqual(default, chatJoinRequest.Date);
        Assert.Equal(_fixture.SupergroupChat.Id, chatJoinRequest.Chat.Id);
        Assert.Equal(chatJoinRequest.From.Id, _classFixture.RegularMemberUserId);

        _classFixture.ChatJoinRequest = chatJoinRequest;
    }

    [OrderedFact("Should decline chat join request")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeclineChatJoinRequest)]
    public async Task Should_Decline_Chat_Join_Request()
    {

        Exception exception = await Record.ExceptionAsync(async () =>
            await BotClient.DeclineChatJoinRequest(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId
            )
        );
        Assert.Null(exception);
    }

    [OrderedFact("Should approve chat join request")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.ApproveChatJoinRequest)]
    public async Task Should_Approve_Chat_Join_Request()
    {
        Update update = await _fixture.UpdateReceiver.GetUpdateAsync(
            predicate: u => u.ChatJoinRequest is not null,
            updateTypes: UpdateType.ChatJoinRequest
        );

        ChatJoinRequest chatJoinRequest = update.ChatJoinRequest;

        Assert.NotNull(chatJoinRequest);
        Assert.NotNull(chatJoinRequest.InviteLink);
        Assert.NotNull(chatJoinRequest.Chat);
        Assert.NotNull(chatJoinRequest.From);
        Assert.NotEqual(default, chatJoinRequest.Date);
        Assert.Equal(_fixture.SupergroupChat.Id, chatJoinRequest.Chat.Id);
        Assert.Equal(chatJoinRequest.From.Id, _classFixture.RegularMemberUserId);

        Exception exception = await Record.ExceptionAsync(async () =>
            await BotClient.ApproveChatJoinRequest(
                chatId: _fixture.SupergroupChat.Id,
                userId: _classFixture.RegularMemberUserId
            )
        );
        Assert.Null(exception);
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

        Assert.Equal(ChatMemberStatus.Administrator, newChatMember.Status);
        ChatMemberAdministrator administrator = Assert.IsType<ChatMemberAdministrator>(newChatMember);

        Assert.Equal("CHANGED TITLE", administrator.CustomTitle);

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

    [OrderedFact("Should get banned chat member with restricted status and of ChatMemberRestricted type")]
    public async Task Should_Get_Chat_Member_Restricted()
    {
        ChatMember chatMember = await BotClient.GetChatMemberAsync(
            chatId: _fixture.SupergroupChat,
            userId: _classFixture.RegularMemberUserId
        );

        Assert.Equal(ChatMemberStatus.Restricted, chatMember.Status);
        ChatMemberRestricted restrictedMember = Assert.IsType<ChatMemberRestricted>(chatMember);
        Assert.NotNull(restrictedMember.UntilDate);
        Assert.False(restrictedMember.CanSendOtherMessages);
    }

    #endregion

    #region Receving chat member status update

    [OrderedFact("Should receive chat member updated")]
    public async Task Should_Receive_Chat_Member_Updated()
    {
        await _fixture.SendTestInstructionsAsync(
            $"Chat admin should kick @{_classFixture.RegularMemberUserName.Replace("_", @"\_")}."
        );
        Update[] updates = await _fixture.UpdateReceiver
            .GetUpdatesAsync(
                predicate: u => u.ChatMember?.Chat.Id == _fixture.SupergroupChat.Id,
                updateTypes: UpdateType.ChatMember
            )
            .ConfigureAwait(false);

        Update update = updates.Single();

        await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

        ChatMemberUpdated chatMemberUpdated = update.ChatMember;

        Assert.NotNull(chatMemberUpdated);
        Assert.NotNull(chatMemberUpdated.OldChatMember);
        Assert.NotNull(chatMemberUpdated.NewChatMember);

        Assert.True(chatMemberUpdated.OldChatMember.Status == ChatMemberStatus.Restricted);
        Assert.True(chatMemberUpdated.NewChatMember.Status == ChatMemberStatus.Kicked);

        Assert.IsType<ChatMemberRestricted>(chatMemberUpdated.OldChatMember);
        ChatMemberBanned newChatMember = Assert.IsType<ChatMemberBanned>(chatMemberUpdated.NewChatMember);

        Assert.Null(newChatMember.UntilDate);
        Assert.Equal(_classFixture.RegularMemberUserId, newChatMember.User.Id);
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

        await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

        Update _ = await _fixture.UpdateReceiver
            .GetUpdateAsync(
                u => u.Message?.Chat.Id == _fixture.SupergroupChat.Id &&
                     u.Message.Type == MessageType.ChatMembersAdded,
                updateTypes: UpdateType.Message,
                cancellationToken: cts.Token
            );

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

        await BotClient.BanChatMemberAsync(
            chatId: _fixture.SupergroupChat.Id,
            userId: _classFixture.RegularMemberUserId,
            untilDate: DateTime.UtcNow.AddSeconds(banSeconds)
        );
    }

    [OrderedFact("Should get banned chat member with restricted status and of ChatMemberBanned type with not null UntilDate")]
    public async Task Should_Get_Chat_Member_Restricted_With_Until_Date()
    {
        ChatMember chatMember = await BotClient.GetChatMemberAsync(
            chatId: _fixture.SupergroupChat,
            userId: _classFixture.RegularMemberUserId
        );

        Assert.Equal(ChatMemberStatus.Kicked, chatMember.Status);
        ChatMemberBanned restrictedMember = Assert.IsType<ChatMemberBanned>(chatMember);
        Assert.NotNull(restrictedMember.UntilDate);
    }

    #endregion
}