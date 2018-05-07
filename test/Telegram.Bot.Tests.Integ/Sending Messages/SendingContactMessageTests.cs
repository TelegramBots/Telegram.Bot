using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.SendContactMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class SendingContactMessageTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public SendingContactMessageTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendContact)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendContact)]
        public async Task Should_Send_Contact()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendContact);

            const string phoneNumber = "+1234567890";
            const string firstName = "Han";
            const string lastName = "Solo";

            Message message = await BotClient.SendContactAsync(
                chatId: _fixture.SupergroupChat,
                phoneNumber: phoneNumber,
                firstName: firstName,
                lastName: lastName
            );

            Assert.Equal(MessageType.Contact, message.Type);
            Assert.Equal(phoneNumber, message.Contact.PhoneNumber);
            Assert.Equal(firstName, message.Contact.FirstName);
            Assert.Equal(lastName, message.Contact.LastName);
        }

        private static class FactTitles
        {
            public const string ShouldSendContact = "Should send a contact";
        }
    }
}
