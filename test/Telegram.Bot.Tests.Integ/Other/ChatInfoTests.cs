using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.ChatInfo)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class ChatInfoTests(TestsFixture fixture)
{
    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should get supergroup chat info")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
    public async Task Should_Get_Supergroup_Chat()
    {
        Chat supergroupChat = fixture.SupergroupChat;

        Chat chat = await BotClient.GetChatAsync(
            new GetChatRequest
            {
                ChatId = supergroupChat.Id,
            }
        );

        Assert.Equal(ChatType.Supergroup, chat.Type);
        Assert.Equal(supergroupChat.Id, chat.Id);
        Assert.Equal(supergroupChat.Title, chat.Title);
        Assert.Equal(supergroupChat.Username, chat.Username);
        Assert.Equal(supergroupChat.Description, chat.Description);
        // Don't compare invite links, it's easy to invalidate them accidentally so the test
        // fails for no good reason
        // Assert.Equal(supergroupChat.InviteLink, chat.InviteLink);
        Assert.Equal(supergroupChat.PinnedMessage, chat.PinnedMessage);
        Assert.Equal(supergroupChat.StickerSetName, chat.StickerSetName);
        Assert.Equal(supergroupChat.CanSetStickerSet, chat.CanSetStickerSet);
        Assert.Null(chat.FirstName);
        Assert.Null(chat.LastName);
        Assert.NotNull(chat.Permissions);
    }

    [OrderedFact("Should get chat member: bot(admin)")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChatMember)]
    public async Task Should_Get_Bot_Chat_Member()
    {
        ChatMember memberBot = await BotClient.GetChatMemberAsync(
            new()
            {
                ChatId = fixture.SupergroupChat.Id,
                UserId = fixture.BotUser.Id,
            }
        );

        Assert.Equal(ChatMemberStatus.Administrator, memberBot.Status);
        ChatMemberAdministrator administrator = Assert.IsType<ChatMemberAdministrator>(memberBot);

        Assert.True(administrator.CanChangeInfo);
        Assert.True(administrator.CanDeleteMessages);
        Assert.True(administrator.CanInviteUsers);
        Assert.True(administrator.CanPromoteMembers);
        Assert.True(administrator.CanRestrictMembers);
        Assert.True(administrator.CanPinMessages);
        Assert.False(administrator.CanBeEdited);
        // Channels only
        Assert.Null(administrator.CanPostMessages);
        Assert.Null(administrator.CanEditMessages);

        Asserts.UsersEqual(fixture.BotUser, memberBot.User);
    }

    [OrderedFact("Should get supergroup chat administrators")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChatAdministrators)]
    public async Task Should_Get_Chat_Admins()
    {
        ChatMember[] chatAdmins = await BotClient.GetChatAdministratorsAsync(
            new GetChatAdministratorsRequest
            {
                ChatId = fixture.SupergroupChat.Id,
            }
        );

        ChatMember memberCreator = Assert.Single(chatAdmins, admin => admin.Status == ChatMemberStatus.Creator);
        Assert.IsType<ChatMemberOwner>(memberCreator);

        ChatMember memberBot = Assert.Single(chatAdmins, admin => admin.User.IsBot);
        Debug.Assert(memberBot != null);

        Assert.True(2 <= chatAdmins.Length); // at least, Bot and the Creator
        Asserts.UsersEqual(fixture.BotUser, memberBot.User);
    }

    [OrderedFact("Should get private chat info")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChatAdministrators)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChat)]
    public async Task Should_Get_Private_Chat()
    {
        long privateChatId;
        {
            /* In order to have a private chat id, take the Creator of supergroup and use his User ID because
             * for a regular user, "User ID" is the same number as "Private Chat ID".
             */
            ChatMember[] chatAdmins = await BotClient.GetChatAdministratorsAsync(
                new GetChatAdministratorsRequest { ChatId = fixture.SupergroupChat }
            );
            privateChatId = chatAdmins
                .Single(member => member.Status == ChatMemberStatus.Creator)
                .User.Id;
        }

        Chat chat = await BotClient.GetChatAsync(
            new GetChatRequest { ChatId = privateChatId, }
        );

        Assert.Equal(ChatType.Private, chat.Type);
        Assert.Equal(privateChatId, chat.Id);

        // Mandatory fields:
        Assert.NotNull(chat.Username);
        Assert.NotNull(chat.FirstName);
        Assert.NotEmpty(chat.Username);
        Assert.NotEmpty(chat.FirstName);

        // Following fields of a chat do not apply to a private chat:
        Assert.Null(chat.Title);
        Assert.Null(chat.Description);
        Assert.Null(chat.InviteLink);
        Assert.Null(chat.PinnedMessage);
        Assert.Null(chat.StickerSetName);
        Assert.Null(chat.CanSetStickerSet);
    }

    [OrderedFact("Should get chat members count")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetChatMembersCount)]
    public async Task Should_Get_Chat_Members_Count()
    {
        int membersCount = await BotClient.GetChatMemberCountAsync(
             new GetChatMemberCountRequest {ChatId = fixture.SupergroupChat.Id}
        );

        Assert.True(2 <= membersCount); // at least, Bot and the Creator
    }

    /// <remarks>
    /// The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear
    /// its typing status)
    /// </remarks>
    [OrderedFact("Should send action to chat: recording voice")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendChatAction)]
    public async Task Should_Send_Chat_Action()
    {
        await BotClient.SendChatActionAsync(
            new()
            {
                ChatId = fixture.SupergroupChat.Id,
                Action = ChatAction.RecordVoice,
            }
        );

        await Task.Delay(5_000);
    }
}
