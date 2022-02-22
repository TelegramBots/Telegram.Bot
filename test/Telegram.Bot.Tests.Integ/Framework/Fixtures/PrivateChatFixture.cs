using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Framework.Fixtures;

public class PrivateChatFixture : AsyncLifetimeFixture
{
    public Chat PrivateChat { get; private set; }

    public PrivateChatFixture(TestsFixture testsFixture, string collectionName)
    {
        AddLifetime(
            initialize: async () =>
            {
                testsFixture.PrivateChat ??= await GetChat(testsFixture, collectionName);
                PrivateChat = testsFixture.PrivateChat;

                await testsFixture.SendTestCollectionNotificationAsync(
                    collectionName,
                    $"Tests will be executed in chat with @{PrivateChat.GetSafeUsername()}"
                );
            }
        );
    }

    static async Task<Chat> GetChat(TestsFixture testsFixture, string collectionName)
    {
        Chat chat;
        long? chatId = testsFixture.Configuration.TesterPrivateChatId;
        if (chatId.HasValue)
        {
            chat = await testsFixture.BotClient.GetChatAsync(chatId);
        }
        else
        {
            await testsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

            string botUsername = testsFixture.BotUser.GetSafeUsername();
            await testsFixture.SendTestCollectionNotificationAsync(collectionName,
                $"No value is set for `{nameof(TestConfiguration.TesterPrivateChatId)}` in test " +
                $"settings. Tester should send /test command in private chat with @{botUsername}."
            );

            chat = await testsFixture.GetChatFromTesterAsync(ChatType.Private);
        }
        return chat;
    }
}