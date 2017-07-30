using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.SendingMessages
{
    [Collection(CommonConstants.TestCollections.SendingMessages)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class MessageSendingTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public MessageSendingTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldSendTextMessage)]
        [ExecutionOrder(1.1)]
        public async Task ShouldSendTextMessage()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendTextMessage);

            const string text = "Hello world!";
            Message message = await BotClient.SendTextMessageAsync(_fixture.SuperGroupChatId, text);

            Assert.Equal(text, message.Text);
            Assert.Equal(MessageType.TextMessage, message.Type);
            Assert.Equal(_fixture.SuperGroupChatId.ToString(), message.Chat.Id.ToString());
        }

        private static class FactTitles
        {
            public const string ShouldSendTextMessage = "Should send text message";
        }
    }
}
