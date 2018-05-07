using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Exceptions
{
    [Collection(Constants.TestCollections.Exceptions)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ApiExceptionsTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public ApiExceptionsTests(TestsFixture fixture)
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

        [OrderedFact(DisplayName = FactTitles.ShouldThrowExceptionChatNotInitiatedException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        public async Task Should_Throw_Exception_ChatNotInitiatedException()
        {
            //ToDo add exception. forward message from another bot. Forbidden: bot can't send messages to bots
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionChatNotInitiatedException,
                "Forward a message to this chat from a user that never started a chat with this bot");

            Update forwardedMessageUpdate = (await _fixture.UpdateReceiver.GetUpdatesAsync(u =>
                    u.Message.ForwardFrom != null, updateTypes: UpdateType.Message
            )).Single();
            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync();

            ForbiddenException e = await Assert.ThrowsAnyAsync<ForbiddenException>(() =>
                BotClient.SendTextMessageAsync(
                    forwardedMessageUpdate.Message.ForwardFrom.Id,
                    $"Error! If you see this message, talk to @{forwardedMessageUpdate.Message.From.Username}"
                )
            );

            Assert.IsType<ChatNotInitiatedException>(e);
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

        [OrderedFact(DisplayName = FactTitles.ShouldThrowExceptionInvalidQueryIdException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        public async Task Should_Throw_Exception_QueryIdInvalidException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionInvalidQueryIdException,
                 "Start typing to invoke inline request",
                startInlineQuery: true);

            Update queryUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] results =
            {
                new InlineQueryResultArticle(
                    id: "article:bot-api",
                    title: "Telegram Bot API",
                    inputMessageContent: new InputTextMessageContent("https://core.telegram.org/bots/api"))
                {
                    Description = "The Bot API is an HTTP-based interface created for developers",
                },
            };

            await Task.Delay(10_000);

            InvalidQueryIdException e = await Assert.ThrowsAnyAsync<InvalidQueryIdException>(() =>
                BotClient.AnswerInlineQueryAsync(
                    inlineQueryId: queryUpdate.InlineQuery.Id,
                    results: results,
                    cacheTime: 0
                )
            );

            Assert.Equal("inline_query_id", e.Parameter);
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

            public const string ShouldThrowExceptionMessageIsNotModifiedException =
                "Should throw MessageIsNotModifiedException while editing previously sent message " +
                "with the same text";

            public const string ShouldThrowExceptionInvalidQueryIdException =
                "Should throw InvalidQueryIdException when AnswerInlineQueryAsync called with" +
                " 10 second delay";
        }
    }
}
