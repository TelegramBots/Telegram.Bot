using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.ReplyMarkups.Buttons;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.MessageReplyMarkup)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class MessageReplyMarkupTests : IClassFixture<MessageReplyMarkupTests.Fixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly Fixture _classFixture;

        private readonly TestsFixture _fixture;

        public MessageReplyMarkupTests(TestsFixture testsFixture, Fixture fixture)
        {
            _fixture = testsFixture;
            _classFixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldReceiveContactInfo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(1)]
        public async Task Should_Receive_Contact_Info()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldReceiveContactInfo);

            IReplyMarkup replyMarkup = new ReplyKeyboardMarkup(
                keyboardRow: new[] { new RequestContactButton("Share Contact") },
                resizeKeyboard: true,
                oneTimeKeyboard: true
            );

            await BotClient.SendTextMessageAsync(
                chatId: _classFixture.PrivateChat,
                text: "Share your contact info using the keyboard reply markup provided.",
                replyMarkup: replyMarkup
            );

            Message contactMessage = await GetContactMessageFromChat(_classFixture.PrivateChat.Id);

            Assert.NotEmpty(contactMessage.Contact.FirstName);
            Assert.NotEmpty(contactMessage.Contact.PhoneNumber);
            Assert.Equal(_classFixture.PrivateChat.Id, contactMessage.Contact.UserId);

            await BotClient.SendTextMessageAsync(
                chatId: _classFixture.PrivateChat,
                text: "Got it. Removing reply keyboard markup...",
                replyMarkup: new ReplyKeyboardRemove()
            );
        }

        private Task<Message> GetContactMessageFromChat(long chatId) =>
            _fixture.UpdateReceiver.GetUpdatesAsync(
                predicate: u => u.Message.Type == MessageType.ContactMessage &&
                                u.Message.Chat.Id == chatId,
                updateTypes: UpdateType.MessageUpdate
            )
            .ContinueWith(t => t.Result.Single().Message);

        private static class FactTitles
        {
            public const string ShouldReceiveContactInfo = "Should get contact info from keyboard reply markup";
        }

        public class Fixture : PrivateChatFixture
        {
            public Fixture(TestsFixture testsFixture)
                : base(testsFixture, Constants.TestCollections.MessageReplyMarkup)
            { }
        }
    }
}
