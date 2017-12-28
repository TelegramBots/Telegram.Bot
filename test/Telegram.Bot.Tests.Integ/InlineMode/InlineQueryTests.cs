using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Xunit;

namespace Telegram.Bot.Tests.Integ.InlineMode
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
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithArticle,
                $"Start an inline query with bot. For example, type `@{_fixture.BotUser.Username}` in chat and wait.");

            Update update = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResult[] results = new InlineQueryResult[]
            {
                new InlineQueryResultArticle
                {
                    Id = "bot-api",
                    Title = "Telegram Bot API",
                    Description = "The Bot API is an HTTP-based interface created for developers",
                    InputMessageContent = new InputTextMessageContent
                    {
                        MessageText = "https://core.telegram.org/bots/api",
                    },
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
