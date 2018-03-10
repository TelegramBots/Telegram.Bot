using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Framework.Fixtures
{
    public class ChannelChatFixture
    {
        public Chat ChannelChat { get; }

        public string ChannelChatId { get; private set; }

        private readonly TestsFixture _testsFixture;

        public ChannelChatFixture(TestsFixture testsFixture, string collectionName)
        {
            _testsFixture = testsFixture;

            if (_testsFixture.ChannelChat == null)
            {
                _testsFixture.ChannelChat = GetChat(collectionName).GetAwaiter().GetResult();
            }
            ChannelChat = _testsFixture.ChannelChat;

            ChannelChatId = ChannelChat.Username == null
                ? ChannelChat.Id.ToString()
                : '@' + ChannelChat.Username;

            _testsFixture.SendTestCollectionNotificationAsync(
                collectionName,
                $"Tests will be executed in channel {ChannelChatId.Replace("_", @"\_")}"
            ).GetAwaiter().GetResult();
        }

        private async Task<Chat> GetChat(string collectionName)
        {
            Chat chat;
            string chatId = ConfigurationProvider.TestConfigurations.ChannelChatId;
            if (chatId == null)
            {
                await _testsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

                string botUserName = _testsFixture.BotUser.Username;
                await _testsFixture.SendTestCollectionNotificationAsync(collectionName,
                    "No channel is set in test settings. Tester should forward a message from a channel " +
                    $"so bot can run tests there. @{botUserName} must be an admin in that channel."
                );

                chat = await _testsFixture.GetChatFromTesterAsync(ChatType.Channel);
            }
            else
            {
                chat = await _testsFixture.BotClient.GetChatAsync(chatId);
            }
            return chat;
        }
    }
}
