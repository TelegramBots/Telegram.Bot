using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Framework.Fixtures;

public class PrivateChatFixture : AsyncLifetimeFixture
{
    public ChatFullInfo PrivateChat { get; private set; }

    public PrivateChatFixture(TestsFixture testsFixture, string collectionName)
    {
        AddInitializer(
            async () =>
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

    static async Task<ChatFullInfo> GetChat(TestsFixture testsFixture, string collectionName)
    {
        ChatFullInfo chat;
        long? chatId = testsFixture.Configuration.TesterPrivateChatId;
        if (chatId.HasValue)
        {
            chat = await testsFixture.BotClient.GetChat(chatId);
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