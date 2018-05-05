using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages
{
    [Collection(Constants.TestCollections.DeleteMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer2, Constants.AssemblyName)]
    public class DeleteMessageTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public DeleteMessageTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldDeleteMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.DeleteMessage)]
        public async Task Should_Delete_Message()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDeleteMessage);

            Message message = await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat.Id,
                text: "This message will be deleted shortly"
            );

            await Task.Delay(1_000);

            await BotClient.DeleteMessageAsync(
                chatId: message.Chat.Id,
                messageId: message.MessageId
            );
        }

        [OrderedFact(DisplayName = FactTitles.ShouldDeleteMessageFromInlineQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
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
                },
                cacheTime: 0
            );

            (Update MessageUpdate, Update ChosenResultUpdate) = await _fixture.UpdateReceiver.GetInlineQueryResultUpdates(MessageType.Text);

            await Task.Delay(1_000);

            await BotClient.DeleteMessageAsync(
                chatId: MessageUpdate.Message.Chat.Id,
                messageId: MessageUpdate.Message.MessageId
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
