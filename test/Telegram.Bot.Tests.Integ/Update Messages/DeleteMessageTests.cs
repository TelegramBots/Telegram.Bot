using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages
{
    [Collection(Constants.TestCollections.DeleteMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class DeleteMessageTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public DeleteMessageTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldDeleteMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteMessage)]
        [ExecutionOrder(1)]
        public async Task Should_Delete_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteMessage);

            Message message = await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat.Id,
                text: "This message will be deleted shortly"
            );

            await Task.Delay(500);

            await BotClient.DeleteMessageAsync(
                chatId: message.Chat.Id,
                messageId: message.MessageId
            );
        }

        [Fact(DisplayName = FactTitles.ShouldDeleteMessageFromInlineQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [ExecutionOrder(2)]
        public async Task Should_Delete_Message_From_InlineQuery()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteMessageFromInlineQuery,
                startInlineQuery: true);

            Update queryUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            await BotClient.AnswerInlineQueryAsync(
                inlineQueryId: queryUpdate.InlineQuery.Id,
                results: new[]
                {
                    new InlineQueryResultArticle(
                        id: "article-to-delete",
                        title: "Telegram Bot API",
                        inputMessageContent: new InputTextMessageContent("https://www.telegram.org/")
                    )
                }
            );

            var inlieQueryUpdates = await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(MessageType.Text);

            await Task.Delay(500);

            await BotClient.DeleteMessageAsync(
                chatId: inlieQueryUpdates.MessageUpdate.Message.Chat.Id,
                messageId: inlieQueryUpdates.MessageUpdate.Message.MessageId
            );
        }

        private static class FactTitles
        {
            public const string ShouldDeleteMessage = "Should delete message";

            public const string ShouldDeleteMessageFromInlineQuery =
                "Should delete message generated from an inline query result";
        }
    }
}