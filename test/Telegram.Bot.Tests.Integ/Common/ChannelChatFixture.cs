using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Common
{
    public abstract class ChannelChatFixture
    {
        public TestsFixture TestsFixture { get; }

        public Chat ChannelChat { get; }

        public string ChannelChatId { get; }

        private static Chat _chat;

        protected ChannelChatFixture(TestsFixture testsFixture, string collectionName)
        {
            TestsFixture = testsFixture;

            ChannelChat = GetChannelChat(collectionName, testsFixture.BotUser.Username)
                .GetAwaiter().GetResult();

            ChannelChatId = ChannelChat.Username is default
                ? ChannelChat.Id.ToString()
                : '@' + ChannelChat.Username;
        }

        private async Task<Chat> GetChannelChat(string collectionName, string botUserName)
        {
            if (_chat is default)
            {
                string chatId = ConfigurationProvider.TestConfigurations.ChannelChatId;
                if (chatId is default)
                {
                    await TestsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

                    await TestsFixture.SendTestCollectionNotificationAsync(collectionName,
                        "No channel is set in test settings. Tester should forward a message from a channel " +
                        $"so bot can run tests there. @{botUserName} must be an admin in that channel."
                    );

                    _chat = await TestsFixture.GetChatFromTesterAsync(ChatType.Channel);
                }
                else
                {
                    _chat = await TestsFixture.BotClient.GetChatAsync(chatId);
                }
            }

            return _chat;
        }
    }
}
