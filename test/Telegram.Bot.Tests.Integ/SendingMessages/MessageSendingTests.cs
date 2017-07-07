using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.SendingMessages
{
    [Collection(CommonConstants.TestCollections.SendingMessages)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    [Trait(CommonConstants.CategoryTraitName, CommonConstants.TestCategories.SendingMessages)]
    public class MessageSendingTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly BotClientFixture _fixture;

        public MessageSendingTests(BotClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldSendTextMessage)]
        [ExecutionOrder(1.1)]
        public async Task ShouldSendTextMessage()
        {
            await _fixture.SendTestCaseNotification(FactTitles.ShouldSendTextMessage);

            const string text = "Hello world!";
            Message message = await BotClient.SendTextMessageAsync(_fixture.ChatId, text);

            Assert.Equal(text, message.Text);
            Assert.Equal(MessageType.TextMessage, message.Type);
            Assert.Equal(_fixture.ChatId.ToString(), message.Chat.Id.ToString());
        }

        private static class FactTitles
        {
            public const string ShouldSendTextMessage = "Should send text message";
        }
    }
}
