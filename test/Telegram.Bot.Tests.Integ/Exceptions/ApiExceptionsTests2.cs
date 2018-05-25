using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Exceptions
{
    [Collection(Constants.TestCollections.Exceptions2)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ApiExceptionsTests2
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public ApiExceptionsTests2(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowChatNotFoundException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        public async Task Should_Throw_Exception_ChatNotFoundException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowChatNotFoundException);

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.SendTextMessageAsync(0, "test")
            );

            Assert.IsType<ChatNotFoundException>(e);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowInvalidUserIdException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        public async Task Should_Throw_Exception_InvalidUserIdException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidUserIdException);

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.PromoteChatMemberAsync(_fixture.SupergroupChat.Id, 123456)
            );

            Assert.IsType<InvalidUserIdException>(e);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowExceptionContactRequestException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        public async Task Should_Throw_Exception_ContactRequestException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionContactRequestException);

            ReplyKeyboardMarkup replyMarkup = new ReplyKeyboardMarkup(new[]
            {
                KeyboardButton.WithRequestContact("Share Contact"),
            });

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.SendTextMessageAsync(
                    _fixture.SupergroupChat.Id,
                    "You should never see this message",
                    replyMarkup: replyMarkup)
            );

            Assert.IsType<ContactRequestException>(e);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowExceptionMessageIsNotModifiedException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        public async Task Should_Throw_Exception_MessageIsNotModifiedException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionMessageIsNotModifiedException);

            const string messageTextToModify = "Message text to modify";
            Message message = await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat.Id,
                messageTextToModify);

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.EditMessageTextAsync(
                    _fixture.SupergroupChat.Id,
                    message.MessageId,
                    messageTextToModify
                )
            );

            Assert.IsType<MessageIsNotModifiedException>(e);
        }

        private static class FactTitles
        {
            public const string ShouldThrowChatNotFoundException =
                "Should throw ChatNotFoundException while trying to send message to an invalid chat";

            public const string ShouldThrowInvalidUserIdException =
                "Should throw InvalidUserIdException while trying to promote an invalid user id";

            public const string ShouldThrowExceptionContactRequestException =
                "Should throw ContactRequestException while asking for user's phone number in non-private " +
                "chat via reply keyboard markup";

            public const string ShouldThrowExceptionMessageIsNotModifiedException =
                "Should throw MessageIsNotModifiedException while editing previously sent message " +
                "with the same text";
        }
    }
}
