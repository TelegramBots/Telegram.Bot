using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Exceptions
{
    [Collection(Constants.TestCollections.Exceptions)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ApiExceptionsTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public ApiExceptionsTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldThrowChatNotFoundException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(1)]
        public async Task Should_Throw_Exception_ChatNotFoundException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowChatNotFoundException);

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.SendTextMessageAsync(0, "test"));

            Assert.IsType<ChatNotFoundException>(e);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowInvalidUserIdException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(2)]
        public async Task Should_Throw_Exception_InvalidUserIdException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidUserIdException);

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.PromoteChatMemberAsync(_fixture.SuperGroupChatId, 123456));

            Assert.IsType<InvalidUserIdException>(e);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowExceptionChatNotInitiatedException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(3)]
        public async Task Should_Throw_Exception_ChatNotInitiatedException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionChatNotInitiatedException,
                "Forward a message to this chat from a user that never started a chat with this bot");

            Update forwardedMessageUpdate = (await _fixture.UpdateReceiver.GetUpdatesAsync(u =>
                u.Message.IsForwarded, updateTypes: UpdateType.MessageUpdate
            )).Single();
            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

            ForbiddenException e = await Assert.ThrowsAnyAsync<ForbiddenException>(() =>
                BotClient.SendTextMessageAsync(forwardedMessageUpdate.Message.ForwardFrom.Id,
                $"Error! If you see this message, talk to @{forwardedMessageUpdate.Message.From.Username}"));

            Assert.IsType<ChatNotInitiatedException>(e);
        }

        [Fact(DisplayName = FactTitles.ShouldThrowExceptionContactRequestException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(4)]
        public async Task Should_Throw_Exception_ContactRequestException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionContactRequestException);

            ReplyKeyboardMarkup replyMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("Share Contact") { RequestContact = true },
            });

            BadRequestException e = await Assert.ThrowsAnyAsync<BadRequestException>(() =>
                BotClient.SendTextMessageAsync(_fixture.SuperGroupChatId, "You should never see this message",
                replyMarkup: replyMarkup));

            Assert.IsType<ContactRequestException>(e);
        }

        private static class FactTitles
        {
            public const string ShouldThrowChatNotFoundException =
                "Should throw ChatNotFoundException while trying to send message to an invalid chat";

            public const string ShouldThrowInvalidUserIdException =
                "Should throw InvalidUserIdException while trying to promote an invalid user id";

            public const string ShouldThrowExceptionChatNotInitiatedException =
                "Should throw ChatNotInitiatedException while trying to send message to a user who hasn't " +
                "started a chat with bot but bot knows about him/her.";

            public const string ShouldThrowExceptionContactRequestException =
                "Should throw ContactRequestException while asking for user's phone number in non-private " +
                "chat via reply keyboard markup";
        }
    }
}
