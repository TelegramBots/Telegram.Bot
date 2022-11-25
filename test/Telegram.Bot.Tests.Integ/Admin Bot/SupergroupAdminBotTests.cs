using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Admin_Bot;

[Collection(Constants.TestCollections.SupergroupAdminBots)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class SupergroupAdminBotTests : IClassFixture<SupergroupAdminBotTestsFixture>
{
    readonly SupergroupAdminBotTestsFixture _classFixture;

    ITelegramBotClient BotClient => _classFixture.TestsFixture.BotClient;

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
        ChatPermissions newDefaultPermissions = new()
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
        ChatPermissions setChatPermissions = supergroup.Permissions!;

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

    [OrderedFact("Should get chat’s pinned message")]
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

        // Wait for chat object to update on Telegram servers
        await Task.Delay(TimeSpan.FromSeconds(5));

        Chat chat = await BotClient.GetChatAsync(_classFixture.Chat.Id);

        Message secondsFromEndPinnedMessage = _classFixture.PinnedMessages[^2];

        Assert.True(JToken.DeepEquals(
            JToken.FromObject(secondsFromEndPinnedMessage),
            JToken.FromObject(chat.PinnedMessage)
        ));
    }

    [OrderedFact("Should unpin first chat message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
    public async Task Should_Unpin_First_Message()
    {
        await BotClient.UnpinChatMessageAsync(
            chatId: _classFixture.Chat.Id,
            messageId: _classFixture.PinnedMessages.First().MessageId
        );
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
        await using Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Photos.Logo);
        await BotClient.SetChatPhotoAsync(
            chatId: _classFixture.Chat.Id,
            photo: new InputFile(stream)
        );
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
        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(
            () => BotClient.DeleteChatPhotoAsync(_classFixture.Chat.Id)
        );

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

        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(() =>
            BotClient.SetChatStickerSetAsync(_classFixture.Chat.Id, setName)
        );

        Assert.Equal(400, exception.ErrorCode);
        Assert.Equal("Bad Request: can't set supergroup sticker set", exception.Message);
    }

    #endregion

    #region 7. Chat invite links management

    [OrderedFact("Should create an invite link to the group")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.CreateChatInviteLink)]
    public async Task Should_Create_Chat_Invite_Link()
    {
        DateTime createdAt = DateTime.UtcNow;

        // Milliseconds are ignored during conversion to unix timestamp since it counts only up to
        // seconds, so for equality to work later on assertion we need to zero out milliseconds
        DateTime expireDate = createdAt.With(new () {Millisecond = 0}).AddHours(1);

        string inviteLinkName = $"Created at {createdAt:yyyy-MM-ddTHH:mm:ss}Z";

        ChatInviteLink chatInviteLink = await BotClient.CreateChatInviteLinkAsync(
            chatId: _classFixture.TestsFixture.SupergroupChat.Id,
            createsJoinRequest: true,
            name: inviteLinkName,
            expireDate: expireDate
        );

        Assert.NotNull(chatInviteLink);
        Assert.NotNull(chatInviteLink.Creator);
        Assert.Equal(_classFixture.TestsFixture.BotUser.Id, chatInviteLink.Creator.Id);
        Assert.Equal(_classFixture.TestsFixture.BotUser.Username, chatInviteLink.Creator.Username);
        Assert.True(chatInviteLink.Creator.IsBot);
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

    [OrderedFact("Should edit previously created invite link to the group")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditChatInviteLink)]
    public async Task Should_Edit_Chat_Invite_Link()
    {
        DateTime editedAt = DateTime.UtcNow;

        // Milliseconds are ignored during conversion to unix timestamp since it counts only up to
        // seconds, so for equality to work later on assertion we need to zero out milliseconds
        DateTime expireDate = editedAt.With(new () {Millisecond = 0}).AddHours(1);

        string inviteLinkName = $"Edited at {editedAt:yyyy-MM-ddTHH:mm:ss}Z";

        ChatInviteLink editedChatInviteLink = await BotClient.EditChatInviteLinkAsync(
            chatId: _classFixture.TestsFixture.SupergroupChat.Id,
            inviteLink: _classFixture.ChatInviteLink.InviteLink,
            createsJoinRequest: false,
            name: inviteLinkName,
            expireDate: expireDate,
            memberLimit: 100
        );

        ChatInviteLink chatInviteLink = _classFixture.ChatInviteLink;

        Assert.NotNull(editedChatInviteLink);
        Assert.NotNull(editedChatInviteLink.Creator);
        Assert.Equal(inviteLinkName, editedChatInviteLink.Name);
        Assert.Equal(expireDate, editedChatInviteLink.ExpireDate);
        Assert.Equal(100, editedChatInviteLink.MemberLimit);

        Assert.Equal(chatInviteLink.InviteLink, editedChatInviteLink.InviteLink);
        Assert.Equal(chatInviteLink.IsPrimary, editedChatInviteLink.IsPrimary);
        Assert.False(editedChatInviteLink.CreatesJoinRequest);
        Assert.Equal(chatInviteLink.PendingJoinRequestCount, editedChatInviteLink.PendingJoinRequestCount);
        Assert.Equal(chatInviteLink.IsRevoked, editedChatInviteLink.IsRevoked);

        _classFixture.ChatInviteLink = editedChatInviteLink;
    }

    #endregion

    [OrderedFact("Should revoke previously edited invite link to the group")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.RevokeChatInviteLink)]
    public async Task Should_Revoke_Chat_Invite_Link()
    {
        ChatInviteLink revokedChatInviteLink = await BotClient.RevokeChatInviteLinkAsync(
            chatId: _classFixture.TestsFixture.SupergroupChat.Id,
            inviteLink: _classFixture.ChatInviteLink.InviteLink
        );

        ChatInviteLink editedChatInviteLink = _classFixture.ChatInviteLink;

        Assert.NotNull(revokedChatInviteLink);
        Assert.NotNull(revokedChatInviteLink.Creator);
        Assert.Equal(editedChatInviteLink.InviteLink, revokedChatInviteLink.InviteLink);
        Assert.Equal(editedChatInviteLink.Name, revokedChatInviteLink.Name);
        Assert.Equal(editedChatInviteLink.ExpireDate, revokedChatInviteLink.ExpireDate);
        Assert.Equal(editedChatInviteLink.IsPrimary, revokedChatInviteLink.IsPrimary);
        Assert.Equal(editedChatInviteLink.MemberLimit, revokedChatInviteLink.MemberLimit);
        Assert.Equal(editedChatInviteLink.CreatesJoinRequest, revokedChatInviteLink.CreatesJoinRequest);
        Assert.Equal(editedChatInviteLink.PendingJoinRequestCount, revokedChatInviteLink.PendingJoinRequestCount);
        Assert.True(revokedChatInviteLink.IsRevoked);
    }
}
