using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Common.Fixtures
{
    public abstract class PrivateChatFixture
    {
        public Chat PrivateChat { get; }

        private static Chat _chat;

        private readonly TestsFixture _testsFixture;

        protected PrivateChatFixture(TestsFixture testsFixture, string collectionName)
        {
            _testsFixture = testsFixture;

            PrivateChat = GetChat(collectionName).GetAwaiter().GetResult();
        }

        private async Task<Chat> GetChat(string collectionName)
        {
            if (_chat is default)
            {
                long? chatId = ConfigurationProvider.TestConfigurations.TesterPrivateChatId;
                if (chatId.HasValue)
                {
                    _chat = await _testsFixture.BotClient.GetChatAsync(chatId);
                }
                else
                {
                    await _testsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

                    string botUserName = _testsFixture.BotUser.Username;
                    await _testsFixture.SendTestCollectionNotificationAsync(collectionName,
                        "No private chat id is set in test settings. Tester should send /test " +
                        $"command in a private chat with @{botUserName}."
                    );

                    _chat = await _testsFixture.GetChatFromTesterAsync(ChatType.Private);
                }

                await _testsFixture.SendTestCollectionNotificationAsync(collectionName,
                        $"Tests will be executed in chat with @{_chat.Username}");
            }

            return _chat;
        }
    }
}
