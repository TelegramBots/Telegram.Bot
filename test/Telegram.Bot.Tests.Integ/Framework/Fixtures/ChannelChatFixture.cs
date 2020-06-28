using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Framework.Fixtures
{
    public class ChannelChatFixture : IAsyncLifetime
    {
        private readonly TestsFixture _testsFixture;
        private readonly string _collectionName;

        public Chat ChannelChat { get; private set; }
        public string ChannelChatId { get; private set; }

        public ChannelChatFixture(TestsFixture testsFixture, string collectionName)
        {
            _testsFixture = testsFixture;
            _collectionName = collectionName;
        }

        public async Task InitializeAsync()
        {
            _testsFixture.ChannelChat ??= await GetChat(_collectionName);

            ChannelChat = _testsFixture.ChannelChat;

            ChannelChatId = ChannelChat.Username is null
                ? ChannelChat.Id.ToString()
                : '@' + ChannelChat.Username;

            await _testsFixture.SendTestCollectionNotificationAsync(
                _collectionName,
                $"Tests will be executed in channel {ChannelChatId.Replace("_", @"\_")}"
            );
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private async Task<Chat> GetChat(string collectionName)
        {
            Chat chat;
            string chatId = ConfigurationProvider.TestConfigurations.ChannelChatId;
            if (chatId is null)
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
