using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Xunit;

namespace Telegram.Bot.Tests.Integ.InlineQuery
{
    [Collection(CommonConstants.TestCollections.InlineQuery)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    [Trait(CommonConstants.CategoryTraitName, CommonConstants.TestCategories.InlineQueries)]
    public class InlineQueryTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly BotClientFixture _fixture;

        public InlineQueryTests(BotClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerInlineQueryWithArticle)]
        [Trait(CommonConstants.CategoryTraitName, CommonConstants.TestCategories.Games)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendMessage)]
        [ExecutionOrder(1.1)]
        public async Task ShouldAnswerInlineQueryWithArticle()
        {
            await _fixture.SendTestCaseNotification(FactTitles.ShouldAnswerInlineQueryWithArticle,
                "Start an inline query with bot. For example, type `@bot_user_name ` in chat and wait.");

            var results = new InlineQueryResult[]
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
            Update update = await GetInlineQueryUpdate();

            bool result = await BotClient.AnswerInlineQueryAsync(
                update.InlineQuery.Id,
                results, 0);

            Assert.True(result);
        }

        private async Task<Update> GetInlineQueryUpdate(CancellationToken cancellationToken = default(CancellationToken))
        {
            var updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
                cancellationToken: cancellationToken,
                updateTypes: UpdateType.InlineQueryUpdate);

            var update = updates.Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

            return update;
        }

        private static class FactTitles
        {
            public const string ShouldAnswerInlineQueryWithArticle = "Should answer inline query with an article";
        }
    }
}
