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
public class SupergroupAdminBotTests(SupergroupAdminBotTestsFixture classFixture)
    : TestClass(classFixture.TestsFixture), IClassFixture<SupergroupAdminBotTestsFixture>
{
    #region 1. Changing Chat Title

    [OrderedFact("Should set chat title")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatTitle)]
    public async Task Should_Set_Chat_Title()
    {
        await BotClient.SetChatTitle(
            chatId: classFixture.Chat.Id,
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
            CanSendMessages = true,
            CanSendAudios = false,
            CanSendDocuments = true,
            CanSendPhotos =false,
            CanSendVideos = false,
            CanSendVideoNotes = false,
            CanSendVoiceNotes = true,
            CanSendPolls = false,
            CanSendOtherMessages = false,
            CanAddWebPagePreviews = false,
            CanChangeInfo = false,
            CanInviteUsers = false,
            CanPinMessages = false,
            CanManageTopics = false,
        };

        await BotClient.SetChatPermissions(classFixture.Chat.Id, newDefaultPermissions);

        ChatFullInfo supergroup = await BotClient.GetChat(classFixture.Chat.Id);
        Assert.NotNull(supergroup.Permissions);
        Asserts.JsonEquals(newDefaultPermissions, supergroup.Permissions);
    }

    #endregion

    #region 3. Changing Chat Description

    [OrderedFact("Should set chat description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
    public async Task Should_Set_Chat_Description()
    {
        await BotClient.SetChatDescription(
            chatId: classFixture.Chat.Id,
            description: "Test Chat Description"
        );
    }

    [OrderedFact("Should delete chat description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
    public async Task Should_Delete_Chat_Description()
    {
        // ToDo: exception Bad Request: chat description is not modified

        await BotClient.SetChatDescription(
            chatId: classFixture.Chat.Id
        );
    }

    #endregion

    #region 4. Pinning Chat Description

    [OrderedFact("Should pin chat message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PinChatMessage)]
    public async Task Should_Pin_Message()
    {
        Message msg1 = await classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This message will be unpinned second");
        Message msg2 = await classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This will be unpinned as group");
        Message msg3 = await classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This will be unpinned with previous one");
        Message msg4 = await classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This message will be unpinned first");

        await BotClient.PinChatMessage(
            chatId: classFixture.Chat.Id,
            messageId: msg1.Id,
            disableNotification: true
        );

        await BotClient.PinChatMessage(
            chatId: classFixture.Chat.Id,
            messageId: msg2.Id,
            disableNotification: true
        );

        await BotClient.PinChatMessage(
            chatId: classFixture.Chat.Id,
            messageId: msg3.Id,
            disableNotification: true
        );

        await BotClient.PinChatMessage(
            chatId: classFixture.Chat.Id,
            messageId: msg4.Id,
            disableNotification: true
        );

        classFixture.PinnedMessages.Add(msg1);
        classFixture.PinnedMessages.Add(msg2);
        classFixture.PinnedMessages.Add(msg3);
        classFixture.PinnedMessages.Add(msg4);
    }

    [OrderedFact("Should get chat’s pinned message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
    public async Task Should_Get_Last_Chat_Pinned_Message()
    {
        Message pinnedMsg = classFixture.PinnedMessages.Last();

        ChatFullInfo chat = await BotClient.GetChat(classFixture.Chat.Id);

        Assert.NotNull(chat.PinnedMessage);
        Asserts.JsonEquals(pinnedMsg, chat.PinnedMessage);
    }

    [OrderedFact("Should unpin last chat message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
    public async Task Should_Unpin_Last_Message()
    {
        await BotClient.UnpinChatMessage(classFixture.Chat.Id);

        // Wait for chat object to update on Telegram servers
        await Task.Delay(TimeSpan.FromSeconds(5));

        ChatFullInfo chat = await BotClient.GetChat(classFixture.Chat.Id);

    Message secondsFromEndPinnedMessage = classFixture.PinnedMessages[^2];

        Assert.NotNull(chat.PinnedMessage);
        Asserts.JsonEquals(secondsFromEndPinnedMessage, chat.PinnedMessage);
    }

    [OrderedFact("Should unpin first chat message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
    public async Task Should_Unpin_First_Message()
    {
        await BotClient.UnpinChatMessage(
            chatId: classFixture.Chat.Id,
            messageId: classFixture.PinnedMessages.First().Id
        );
    }

    [OrderedFact("Should Unpin all Messages")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinAllChatMessages)]
    public async Task Should_Unpin_All_Messages()
    {
        await BotClient.UnpinAllChatMessages(classFixture.Chat);
    }

    [OrderedFact("Should get the chat info without a pinned message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
    public async Task Should_Get_Chat_With_No_Pinned_Message()
    {
        ChatFullInfo chat = await BotClient.GetChat(classFixture.Chat.Id);

        Assert.Null(chat.PinnedMessage);
    }

    #endregion

    #region 5. Changing Chat Photo

    [OrderedFact("Should set chat photo")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatPhoto)]
    public async Task Should_Set_Chat_Photo()
    {
        await using Stream stream = File.OpenRead(Constants.PathToFile.Photos.Logo);
        await BotClient.WithStreams(stream).SetChatPhoto(
            chatId: classFixture.Chat.Id,
            photo: InputFile.FromStream(stream)
        );
    }

    [OrderedFact("Should delete chat photo")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
    public async Task Should_Delete_Chat_Photo()
    {
        await BotClient.DeleteChatPhoto(classFixture.Chat.Id);
    }

    [OrderedFact("Should throw exception in deleting chat photo with no photo currently set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
    public async Task Should_Throw_On_Deleting_Chat_Deleted_Photo()
    {
        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(
            () => BotClient.DeleteChatPhoto(classFixture.Chat.Id)
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
            BotClient.SetChatStickerSet(classFixture.Chat.Id, setName)
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

        // Milliseconds are ignored during conversion to Unix timestamp since it counts only up to
        // seconds, so for equality to work later on assertion we need to zero out milliseconds
        DateTime expireDate = createdAt.With(new () {Millisecond = 0}).AddHours(1);

        string inviteLinkName = $"Created at {createdAt:yyyy-MM-ddTHH:mm:ss}Z";

        ChatInviteLink chatInviteLink = await BotClient.CreateChatInviteLink(
            chatId: classFixture.TestsFixture.SupergroupChat.Id,
            name: inviteLinkName,
            expireDate: expireDate,
            createsJoinRequest: true);

        Assert.NotNull(chatInviteLink);
        Assert.NotNull(chatInviteLink.Creator);
        Assert.Equal(classFixture.TestsFixture.BotUser.Id, chatInviteLink.Creator.Id);
        Assert.Equal(classFixture.TestsFixture.BotUser.Username, chatInviteLink.Creator.Username);
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

        classFixture.ChatInviteLink = chatInviteLink;
    }

    [OrderedFact("Should edit previously created invite link to the group")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditChatInviteLink)]
    public async Task Should_Edit_Chat_Invite_Link()
    {
        DateTime editedAt = DateTime.UtcNow;

        // Milliseconds are ignored during conversion to Unix timestamp since it counts only up to
        // seconds, so for equality to work later on assertion we need to zero out milliseconds
        DateTime expireDate = editedAt.With(new () {Millisecond = 0}).AddHours(1);

        string inviteLinkName = $"Edited at {editedAt:yyyy-MM-ddTHH:mm:ss}Z";

        ChatInviteLink editedChatInviteLink = await BotClient.EditChatInviteLink(
            chatId: classFixture.TestsFixture.SupergroupChat.Id,
            inviteLink: classFixture.ChatInviteLink.InviteLink,
            name: inviteLinkName,
            expireDate: expireDate,
            memberLimit: 100,
            createsJoinRequest: false
        );

        ChatInviteLink chatInviteLink = classFixture.ChatInviteLink;

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

        classFixture.ChatInviteLink = editedChatInviteLink;
    }

    #endregion

    [OrderedFact("Should revoke previously edited invite link to the group")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.RevokeChatInviteLink)]
    public async Task Should_Revoke_Chat_Invite_Link()
    {
        ChatInviteLink revokedChatInviteLink = await BotClient.RevokeChatInviteLink(
            chatId: classFixture.TestsFixture.SupergroupChat.Id,
            inviteLink: classFixture.ChatInviteLink.InviteLink
        );

        ChatInviteLink editedChatInviteLink = classFixture.ChatInviteLink;

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
