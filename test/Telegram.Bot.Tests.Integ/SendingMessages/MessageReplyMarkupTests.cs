using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.SendingMessages
{
    [Collection(Constants.TestCollections.MessageReplyMarkup)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class MessageReplyMarkupTests : IClassFixture<MessageReplyMarkupTestsFixture>
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly MessageReplyMarkupTestsFixture _classFixture;

        private readonly TestsFixture _fixture;

        public MessageReplyMarkupTests(MessageReplyMarkupTestsFixture fixture)
        {
            _classFixture = fixture;
            _fixture = _classFixture.TestsFixture;
        }

        [Fact(DisplayName = FactTitles.ShouldReceiveContactInfo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(1)]
        public async Task Should_Receive_Contact_Info()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldReceiveContactInfo);

            var replyMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("Share Contact") { RequestContact = true },
            }, true, true);

            Message message = await BotClient.SendTextMessageAsync(
                chatId: _classFixture.TesterPrivateChatId,
                text: "Share your contact info using the keyboard reply markup provided.",
                parseMode: ParseMode.Markdown,
                replyMarkup: replyMarkup
            );

            Message contactMessage = (await _fixture.UpdateReceiver.GetUpdatesAsync(u =>
                    u.Message.Chat.Id == _classFixture.TesterPrivateChatId.Identifier &&
                    u.Message.Type == MessageType.ContactMessage,
                updateTypes: UpdateType.MessageUpdate)).First().Message;

            Assert.NotEmpty(contactMessage.Contact.FirstName);
            Assert.NotEmpty(contactMessage.Contact.PhoneNumber);
            Assert.Equal(int.Parse(_classFixture.TesterPrivateChatId), contactMessage.Contact.UserId);

            await BotClient.SendTextMessageAsync(
                chatId: _classFixture.TesterPrivateChatId,
                text: "Got it. Removing reply keyboard markup...",
                replyMarkup: new ReplyKeyboardRemove()
            );
        }

        private static class FactTitles
        {
            public const string ShouldReceiveContactInfo = "Should get contact info from keyboard reply markup";
        }
    }
}
