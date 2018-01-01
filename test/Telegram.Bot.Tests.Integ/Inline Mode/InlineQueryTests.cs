using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Inline_Mode
{
    [Collection(Constants.TestCollections.InlineQuery)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class InlineQueryTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public InlineQueryTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithArticle)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(1)]
        public async Task Should_Answer_Inline_Query_With_Article()
        {
            // ToDo: add exception: Bad Request: QUERY_ID_INVALID
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithArticle,
                startInlineQuery: true);

            Update update = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InputMessageContent inputMessageContent = new InputTextMessageContent
            {
                MessageText = "https://core.telegram.org/bots/api",
            };

            InlineQueryResult[] results = new InlineQueryResult[]
            {
                new InlineQueryResultArticle(
                    id: "bot-api",
                    title: "Telegram Bot API",
                    inputMessageContent: inputMessageContent)
                {
                    Description = "The Bot API is an HTTP-based interface created for developers",
                },
            };

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: update.InlineQuery.Id,
                results: results,
                cacheTime: 0
            );
        }

        private static class FactTitles
        {
            public const string ShouldAnswerInlineQueryWithArticle = "Should answer inline query with an article";
        }
    }
}
