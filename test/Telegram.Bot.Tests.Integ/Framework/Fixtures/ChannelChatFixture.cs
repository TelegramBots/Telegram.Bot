using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Framework.Fixtures;

public class ChannelChatFixture : AsyncLifetimeFixture
{
    readonly TestsFixture _testsFixture;

    public Chat ChannelChat { get; private set; }
    public ChatId ChannelChatId { get; private set; }

    public ChannelChatFixture(TestsFixture testsFixture, string collectionName)
    {
        _testsFixture = testsFixture;

        AddLifetime(
            initialize: async () =>
            {
                _testsFixture.ChannelChat ??= await GetChat(collectionName);
                ChannelChat = _testsFixture.ChannelChat;

                ChannelChatId = ChannelChat;

                await _testsFixture.SendTestCollectionNotificationAsync(
                    collectionName,
                    $"Tests will be executed in channel {ChannelChatId}"
                );
            }
        );
    }

    async Task<Chat> GetChat(string collectionName)
    {
        var chatId = _testsFixture.Configuration.ChannelChatId;
        if (chatId is not null) return await _testsFixture.BotClient.GetChatAsync(chatId.Value);

        await _testsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

        string botUserName = _testsFixture.BotUser.GetSafeUsername();
        await _testsFixture.SendTestCollectionNotificationAsync(collectionName,
            "No channel is set in test settings. Tester should forward a message from a channel " +
            $"so bot can run tests there. @{botUserName} must be an admin in that channel."
        );

        return await _testsFixture.GetChatFromTesterAsync(ChatType.Channel);
    }
}