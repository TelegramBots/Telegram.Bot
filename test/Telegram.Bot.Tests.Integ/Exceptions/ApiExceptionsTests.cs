using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Exceptions
{
    [Collection(Constants.TestCollections.Exceptions)]
    [Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class ApiExceptionsTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public ApiExceptionsTests(TestsFixture fixture)
        {
            _fixture = fixture;
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

        [OrderedFact(DisplayName = FactTitles.ShouldThrowExceptionInvalidQueryIdException)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        public async Task Should_Throw_Exception_QueryIdInvalidException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowExceptionInvalidQueryIdException,
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
            public const string ShouldThrowExceptionChatNotInitiatedException =
                "Should throw ChatNotInitiatedException while trying to send message to a user who hasn't " +
                "started a chat with bot but bot knows about him/her.";

            public const string ShouldThrowExceptionInvalidQueryIdException =
                "Should throw InvalidQueryIdException when AnswerInlineQueryAsync called with" +
                " 10 second delay";
        }
    }
}
