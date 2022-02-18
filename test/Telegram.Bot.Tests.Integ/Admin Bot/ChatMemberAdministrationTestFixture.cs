using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Admin_Bot;

public class ChatMemberAdministrationTestFixture : IAsyncLifetime
{
    readonly TestsFixture _testsFixture;

    public ChatMemberAdministrationTestFixture(TestsFixture testsFixture) => _testsFixture = testsFixture;

    public Chat RegularMemberChat { get; private set; }
    public long RegularMemberUserId { get; private set; }
    public string RegularMemberUserName { get; private set; }
    public string GroupInviteLink { get; set; }
    public ChatInviteLink ChatInviteLink { get; set; }
    public ChatJoinRequest ChatJoinRequest { get; set; }

    static async Task<Chat> GetChat(TestsFixture testsFixture, string collectionName)
    {
        Chat chat;

        if (testsFixture.Configuration.RegularGroupMemberId is {} userId)
        {
            chat = await testsFixture.BotClient.GetChatAsync(userId);
        }
        else
        {
            await testsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

            await testsFixture.SendTestCollectionNotificationAsync(collectionName,
                $"No value is set for `{nameof(TestConfiguration.RegularGroupMemberId)}` " +
                "in test settings.\n" + "An admin should either forward a message from non-admin member," +
                " send his/her contact or add a non-admin member to the group."
            );

            chat = await testsFixture.GetChatFromAdminAsync();
        }

        if (chat.Username is not null) return chat;

        await testsFixture.SendTestCollectionNotificationAsync(collectionName,
            $"[{chat.FirstName}](tg://user?id={chat.Id}) doesn't have a username.\n" +
            "❎ Failing tests...");

        throw new ArgumentNullException(nameof(chat.Username), "Chat member doesn't have a username");
    }

    public async Task InitializeAsync()
    {
        const string collectionName = Constants.TestCollections.ChatMemberAdministration;

        RegularMemberChat = await GetChat(_testsFixture, collectionName);

        await _testsFixture.SendTestCollectionNotificationAsync(
            collectionName,
            $"Chosen regular member is @{RegularMemberChat.GetSafeUsername()}"
        );

        RegularMemberUserId = RegularMemberChat.Id;
        RegularMemberUserName = RegularMemberChat.Username;
        // Updates from regular user will be received
        _testsFixture.UpdateReceiver.AllowedUsernames.Add(RegularMemberUserName);
    }

    public Task DisposeAsync()
    {
        // Remove regular user from AllowedUserNames
        _testsFixture.UpdateReceiver.AllowedUsernames.Remove(RegularMemberUserName);
        return Task.CompletedTask;
    }
}