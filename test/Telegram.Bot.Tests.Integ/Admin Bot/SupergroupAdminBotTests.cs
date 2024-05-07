using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Admin_Bot;

[Collection(Constants.TestCollections.SupergroupAdminBots)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class SupergroupAdminBotTests(SupergroupAdminBotTestsFixture classFixture)
    : IClassFixture<SupergroupAdminBotTestsFixture>
{
    ITelegramBotClient BotClient => classFixture.TestsFixture.BotClient;

    #region 1. Changing Chat Title

    [OrderedFact("Should set chat title")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatTitle)]
    public async Task Should_Set_Chat_Title()
    {
        await BotClient.SetChatTitleAsync(
            new()
            {
                ChatId = classFixture.Chat.Id,
                Title = "Test Chat Title",
            }
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

        await BotClient.SetChatPermissionsAsync(new()
        {
            ChatId = classFixture.Chat.Id,
            Permissions = newDefaultPermissions,
        });

        Chat supergroup = await BotClient.GetChatAsync(new GetChatRequest { ChatId = classFixture.Chat.Id });
        Assert.NotNull(supergroup.Permissions);
        Asserts.JsonEquals(newDefaultPermissions, supergroup.Permissions);
    }

    #endregion

    #region 3. Changing Chat Description

    [OrderedFact("Should set chat description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
    public async Task Should_Set_Chat_Description()
    {
        await BotClient.SetChatDescriptionAsync(
            new SetChatDescriptionRequest
            {
                ChatId = classFixture.Chat.Id,
                Description = "Test Chat Description",
            }
        );
    }

    [OrderedFact("Should delete chat description")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SetChatDescription)]
    public async Task Should_Delete_Chat_Description()
    {
        // ToDo: exception Bad Request: chat description is not modified

        await BotClient.SetChatDescriptionAsync(
            new SetChatDescriptionRequest { ChatId = classFixture.Chat.Id, }
        );
    }

    #endregion

    #region 4. Pinning Chat Description

    [OrderedFact("Should pin chat message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.PinChatMessage)]
    public async Task Should_Pin_Message()
    {
        Message msg1 = await classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This message will be deleted second");
        Message msg2 = await classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This will be deleted as group");
        Message msg3 = await classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This will be deleted with previous one");
        Message msg4 = await classFixture.TestsFixture.SendTestInstructionsAsync("🧷 This message will be deleted first");

        await BotClient.PinChatMessageAsync(
            new()
            {
                ChatId = classFixture.Chat.Id,
                MessageId = msg1.MessageId,
                DisableNotification = true,
            }
        );

        await BotClient.PinChatMessageAsync(
            new()
            {
                ChatId = classFixture.Chat.Id,
                MessageId = msg2.MessageId,
                DisableNotification = true,
            }
        );

        await BotClient.PinChatMessageAsync(
            new()
            {
                ChatId = classFixture.Chat.Id,
                MessageId = msg3.MessageId,
                DisableNotification = true,
            }
        );

        await BotClient.PinChatMessageAsync(
            new()
            {
                ChatId = classFixture.Chat.Id,
                MessageId = msg4.MessageId,
                DisableNotification = true,
            }
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

        Chat chat = await BotClient.GetChatAsync(new GetChatRequest { ChatId = classFixture.Chat.Id });

        Assert.NotNull(chat.PinnedMessage);
        Asserts.JsonEquals(pinnedMsg, chat.PinnedMessage);
    }

    [OrderedFact("Should unpin last chat message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
    public async Task Should_Unpin_Last_Message()
    {
        await BotClient.UnpinChatMessageAsync(new UnpinChatMessageRequest { ChatId = classFixture.Chat.Id });

        // Wait for chat object to update on Telegram servers
        await Task.Delay(TimeSpan.FromSeconds(5));

        Chat chat = await BotClient.GetChatAsync(new GetChatRequest { ChatId = classFixture.Chat.Id });

    Message secondsFromEndPinnedMessage = classFixture.PinnedMessages[^2];

        Assert.NotNull(chat.PinnedMessage);
        Asserts.JsonEquals(secondsFromEndPinnedMessage, chat.PinnedMessage);
    }

    [OrderedFact("Should unpin first chat message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinChatMessage)]
    public async Task Should_Unpin_First_Message()
    {
        await BotClient.UnpinChatMessageAsync(
            new UnpinChatMessageRequest
            {
                ChatId = classFixture.Chat.Id,
                MessageId = classFixture.PinnedMessages.First().MessageId,
            }
        );
    }

    [OrderedFact("Should Unpin all Messages")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.UnpinAllChatMessages)]
    public async Task Should_Unpin_All_Messages()
    {
        await BotClient.UnpinAllChatMessagesAsync(new() { ChatId = classFixture.Chat });
    }

    [OrderedFact("Should get the chat info without a pinned message")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
    public async Task Should_Get_Chat_With_No_Pinned_Message()
    {
        Chat chat = await BotClient.GetChatAsync(new GetChatRequest { ChatId = classFixture.Chat.Id });

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
            new()
            {
                ChatId = classFixture.Chat.Id,
                Photo = InputFile.FromStream(stream),
            }
        );
    }

    [OrderedFact("Should delete chat photo")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
    public async Task Should_Delete_Chat_Photo()
    {
        await BotClient.DeleteChatPhotoAsync(new DeleteChatPhotoRequest { ChatId = classFixture.Chat.Id });
    }

    [OrderedFact("Should throw exception in deleting chat photo with no photo currently set")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteChatPhoto)]
    public async Task Should_Throw_On_Deleting_Chat_Deleted_Photo()
    {
        ApiRequestException e = await Assert.ThrowsAsync<ApiRequestException>(
            () => BotClient.DeleteChatPhotoAsync(new DeleteChatPhotoRequest { ChatId = classFixture.Chat.Id })
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
            BotClient.SetChatStickerSetAsync(new()
            {
                ChatId = classFixture.Chat.Id,
                StickerSetName = setName,
            })
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

        ChatInviteLink chatInviteLink = await BotClient.CreateChatInviteLinkAsync(
            new CreateChatInviteLinkRequest
            {
                ChatId = classFixture.TestsFixture.SupergroupChat.Id,
                Name = inviteLinkName,
                ExpireDate = expireDate,
                CreatesJoinRequest = true,
            }
        );

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

        ChatInviteLink editedChatInviteLink = await BotClient.EditChatInviteLinkAsync(
            new()
            {
                ChatId = classFixture.TestsFixture.SupergroupChat.Id,
                InviteLink = classFixture.ChatInviteLink.InviteLink,
                Name = inviteLinkName,
                ExpireDate = expireDate,
                MemberLimit = 100,
                CreatesJoinRequest = false,
            }
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
        ChatInviteLink revokedChatInviteLink = await BotClient.RevokeChatInviteLinkAsync(
            new()
            {
                ChatId = classFixture.TestsFixture.SupergroupChat.Id,
                InviteLink = classFixture.ChatInviteLink.InviteLink,
            }
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
