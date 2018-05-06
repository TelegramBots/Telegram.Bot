using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Admin_Bot
{
    public class ChatMemberAdministrationTestFixture
    {
        public Chat RegularMemberChat { get; }

        public int RegularMemberUserId { get; }

        public string RegularMemberUserName { get; }

        public string GroupInviteLink { get; set; }

        public ChatMemberAdministrationTestFixture(TestsFixture testsFixture)
        {
            const string collectionName = Constants.TestCollections.ChatMemberAdministration;

            RegularMemberChat = GetChat(testsFixture, collectionName).GetAwaiter().GetResult();

            testsFixture.SendTestCollectionNotificationAsync(
                collectionName,
                $"Chosen regular member is @{RegularMemberChat.Username.Replace("_", @"\_")}"
            ).GetAwaiter().GetResult();

            RegularMemberUserId = (int)RegularMemberChat.Id;
            RegularMemberUserName = RegularMemberChat.Username;
        }

        private static async Task<Chat> GetChat(TestsFixture testsFixture, string collectionName)
        {
            Chat chat;

            if (int.TryParse(ConfigurationProvider.TestConfigurations.RegularGroupMemberId, out int userId))
            {
                chat = await testsFixture.BotClient.GetChatAsync(userId);
            }
            else
            {
                await testsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

                string botUserName = testsFixture.BotUser.Username;
                await testsFixture.SendTestCollectionNotificationAsync(collectionName,
                    $"No value is set for `{nameof(ConfigurationProvider.TestConfigurations.RegularGroupMemberId)}` " +
                    $"in test settings. A non-admin chat member should send /test command in private chat with " +
                    $"@{botUserName.Replace("_", @"\_")}."
                );

                chat = await testsFixture.GetChatFromTesterAsync(ChatType.Private);
            }

            if (chat.Username == null)
            {
                await testsFixture.SendTestCollectionNotificationAsync(collectionName,
                    $"[{chat.FirstName}](tg://user?id={chat.Id}) doesn't have a username.\n" +
                    "❎ Failing tests...");

                throw new ArgumentNullException(nameof(chat.Username), "Chat member doesn't have a username");
            }

            return chat;
        }
    }
}
