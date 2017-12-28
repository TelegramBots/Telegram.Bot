using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Common.Fixtures
{
    public abstract class ChannelChatFixture
    {
        public Chat ChannelChat { get; }

        public string ChannelChatId { get; }

        private static Chat _chat;

        private readonly TestsFixture _testsFixture;

        protected ChannelChatFixture(TestsFixture testsFixture, string collectionName)
        {
            _testsFixture = testsFixture;

            ChannelChat = GetChat(collectionName).GetAwaiter().GetResult();

            ChannelChatId = ChannelChat.Username is default
                ? ChannelChat.Id.ToString()
                : '@' + ChannelChat.Username;
        }

        private async Task<Chat> GetChat(string collectionName)
        {
            if (_chat is default)
            {
                string chatId = ConfigurationProvider.TestConfigurations.ChannelChatId;
                if (chatId is default)
                {
                    await _testsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

                    string botUserName = _testsFixture.BotUser.Username;
                    await _testsFixture.SendTestCollectionNotificationAsync(collectionName,
                        "No channel is set in test settings. Tester should forward a message from a channel " +
                        $"so bot can run tests there. @{botUserName} must be an admin in that channel."
                    );

                    _chat = await _testsFixture.GetChatFromTesterAsync(ChatType.Channel);
                }
                else
                {
                    _chat = await _testsFixture.BotClient.GetChatAsync(chatId);
                }
            }

            return _chat;
        }
    }
}
