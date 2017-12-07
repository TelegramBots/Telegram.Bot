using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Xunit;

namespace Telegram.Bot.Tests.Integ.InlineQuery
{
    [Collection(Constants.TestCollections.InlineQuery)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class InlineQueryTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public InlineQueryTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithArticle)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(1)]
        public async Task Should_Answer_Inline_Query_With_Article()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerInlineQueryWithArticle,
                "Start an inline query with bot. For example, type `@bot_user_name` in chat and wait.");

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
            Update update = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            bool result = await BotClient.AnswerInlineQueryAsync(
                update.InlineQuery.Id,
                results, 0);

            Assert.True(result);
        }

        private static class FactTitles
        {
            public const string ShouldAnswerInlineQueryWithArticle = "Should answer inline query with an article";
        }
    }
}
