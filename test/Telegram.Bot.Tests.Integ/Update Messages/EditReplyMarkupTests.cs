using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages
{
    [Collection(Constants.TestCollections.EditReplyMarkup)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class EditReplyMarkupTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public EditReplyMarkupTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldEditMessageMarkup)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageReplyMarkup)]
        [ExecutionOrder(1)]
        public async Task Should_Edit_Message_Markup()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditMessageMarkup);

            Message message = await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat.Id,
                text: "Inline keyboard will be updated shortly",
                replyMarkup: (InlineKeyboardMarkup) "Original markup"
            );

            await Task.Delay(500);

            Message editedMessage = await BotClient.EditMessageReplyMarkupAsync(
                chatId: message.Chat.Id,
                messageId: message.MessageId,
                replyMarkup: "Edited 👍"
            );

            Assert.True(JToken.DeepEquals(
                JToken.FromObject(message), JToken.FromObject(editedMessage)
            ));
        }

        [Fact(DisplayName = FactTitles.ShouldEditInlineMessageMarkup)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageReplyMarkup)]
        [ExecutionOrder(2)]
        public async Task Should_Edit_Inline_Message_Markup()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditInlineMessageMarkup,
                startInlineQuery: true);

            #region Answer Inline Query with an Article

            Update inlineQUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            string data = "change-me" + new Random().Next(2_000);
            InlineKeyboardMarkup initialMarkup = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Click here to change this button", data)
            });

            InputMessageContent inputMessageContent = new InputTextMessageContent
            {
                MessageText = "https://core.telegram.org/bots/api"
            };

            InlineQueryResultBase[] inlineQueryResults =
            {
                new InlineQueryResultArticle(
                    id: "bot-api",
                    title: "Telegram Bot API",
                    inputMessageContent: inputMessageContent)
                {
                    Description = "The Bot API is an HTTP-based interface created for developers",
                    ReplyMarkup = initialMarkup,
                },
            };

            await BotClient.AnswerInlineQueryAsync(inlineQUpdate.InlineQuery.Id, inlineQueryResults, 0);

            #endregion

            Update callbackQUpdate = await _fixture.UpdateReceiver
                .GetCallbackQueryUpdateAsync(data: data);

            await BotClient.EditMessageReplyMarkupAsync(
                inlineMessageId: callbackQUpdate.CallbackQuery.InlineMessageId,
                replyMarkup: "✌ Edited 👌"
            );

            Assert.Equal(data, callbackQUpdate.CallbackQuery.Data);
        }

        private static class FactTitles
        {
            public const string ShouldEditMessageMarkup = "Should edit a message's markup";

            public const string ShouldEditInlineMessageMarkup = "Should edit an inline message's markup";
        }
    }
}