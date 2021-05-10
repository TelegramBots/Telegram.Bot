using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Admin_Bot
{
    public class ChatMemberAdministrationTestFixture : AsyncLifetimeFixture
    {
        public Chat RegularMemberChat { get; private set; }
        public long RegularMemberUserId { get; private set; }
        public string RegularMemberUserName { get; private set; }
        public string GroupInviteLink { get; set; }

        public ChatMemberAdministrationTestFixture(TestsFixture testsFixture)
        {
            AddLifetime(
                initialize: async () =>
                {
                    const string collectionName = Constants.TestCollections.ChatMemberAdministration;

                    RegularMemberChat = await GetChat(testsFixture, collectionName);

                    await testsFixture.SendTestCollectionNotificationAsync(
                        collectionName,
                        $"Chosen regular member is @{RegularMemberChat.GetSafeUsername()}"
                    );

                    RegularMemberUserId = RegularMemberChat.Id;
                    RegularMemberUserName = RegularMemberChat.Username;
                    // Updates from regular user will be received
                    testsFixture.UpdateReceiver.AllowedUsernames.Add(RegularMemberUserName);
                }
            );

            // Remove regular user from AllowedUserNames
            AddLifetime(() => testsFixture.UpdateReceiver.AllowedUsernames.Remove(RegularMemberUserName));
        }

        private static async Task<Chat> GetChat(TestsFixture testsFixture, string collectionName)
        {
            Chat chat;

            if (long.TryParse(ConfigurationProvider.TestConfigurations.RegularGroupMemberId, out long userId))
            {
                chat = await testsFixture.BotClient.GetChatAsync(userId);
            }
            else
            {
                await testsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

                await testsFixture.SendTestCollectionNotificationAsync(collectionName,
                    $"No value is set for `{nameof(ConfigurationProvider.TestConfigurations.RegularGroupMemberId)}` " +
                    "in test settings.\n" +
                    "An admin should forward a message from non-admin member or send his/her contact."
                );

                chat = await testsFixture.GetChatFromAdminAsync();
            }

            if (chat.Username is not null) return chat;

            await testsFixture.SendTestCollectionNotificationAsync(collectionName,
                $"[{chat.FirstName}](tg://user?id={chat.Id}) doesn't have a username.\n" +
                "❎ Failing tests...");

            throw new ArgumentNullException(nameof(chat.Username), "Chat member doesn't have a username");
        }
    }
}
