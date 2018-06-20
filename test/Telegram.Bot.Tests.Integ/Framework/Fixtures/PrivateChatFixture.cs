using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Framework.Fixtures
{
    public class PrivateChatFixture
    {
        public Chat PrivateChat { get; }

        private readonly TestsFixture _testsFixture;

        public PrivateChatFixture(TestsFixture testsFixture, string collectionName)
        {
            _testsFixture = testsFixture;

            if (_testsFixture.PrivateChat == null)
            {
                _testsFixture.PrivateChat = GetChat(collectionName).GetAwaiter().GetResult();
            }
            PrivateChat = _testsFixture.PrivateChat;

            _testsFixture.SendTestCollectionNotificationAsync(
                collectionName,
                $"Tests will be executed in chat with @{PrivateChat.Username.Replace("_", @"\_")}"
            ).GetAwaiter().GetResult();
        }

        private async Task<Chat> GetChat(string collectionName)
        {
            Chat chat;
            long? chatId = ConfigurationProvider.TestConfigurations.TesterPrivateChatId;
            if (chatId.HasValue)
            {
                chat = await _testsFixture.BotClient.GetChatAsync(chatId);
            }
            else
            {
                await _testsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

                string botUserName = _testsFixture.BotUser.Username;
                await _testsFixture.SendTestCollectionNotificationAsync(collectionName,
                    $"No value is set for `{nameof(ConfigurationProvider.TestConfigurations.TesterPrivateChatId)}` in test " +
                    $"settings. Tester should send /test command in private chat with @{botUserName.Replace("_", @"\_")}."
                );

                chat = await _testsFixture.GetChatFromTesterAsync(ChatType.Private);
            }
            return chat;
        }
    }
}
