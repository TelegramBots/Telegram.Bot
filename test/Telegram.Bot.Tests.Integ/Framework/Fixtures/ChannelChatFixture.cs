using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Framework.Fixtures
{
    public class ChannelChatFixture
    {
        private readonly TestsFixture _testsFixture;

        public Chat ChannelChat { get; }
        public string ChannelChatId { get; }

        public ChannelChatFixture(TestsFixture testsFixture, string collectionName)
        {
            _testsFixture = testsFixture;

            if (_testsFixture.ChannelChat is null)
            {
                _testsFixture.ChannelChat = GetChat(collectionName).GetAwaiter().GetResult();
            }
            ChannelChat = _testsFixture.ChannelChat;

            ChannelChatId = ChannelChat.Username is null
                ? ChannelChat.Id.ToString()
                : $"@{ChannelChat.GetSafeUsername()}";

            _testsFixture.SendTestCollectionNotificationAsync(
                collectionName,
                $"Tests will be executed in channel {ChannelChatId}"
            ).GetAwaiter().GetResult();
        }

        private async Task<Chat> GetChat(string collectionName)
        {
            string chatId = ConfigurationProvider.TestConfigurations.ChannelChatId;
            if (chatId is not null) return await _testsFixture.BotClient.GetChatAsync(chatId);

            await _testsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

            string botUserName = _testsFixture.BotUser.GetSafeUsername();
            await _testsFixture.SendTestCollectionNotificationAsync(collectionName,
                "No channel is set in test settings. Tester should forward a message from a channel " +
                $"so bot can run tests there. @{botUserName} must be an admin in that channel."
            );

            return await _testsFixture.GetChatFromTesterAsync(ChatType.Channel);
        }
    }
}
