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
    [Collection(Constants.TestCollections.EditMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer2, Constants.AssemblyName)]
    public class EditMessageContentTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public EditMessageContentTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldEditMessageText)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageText)]
        public async Task Should_Edit_Message_Text()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditMessageText);

            Message originalMessage = await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat.Id,
                text: "Message text will be edited shortly."
            );

            await Task.Delay(500);

            const string newText = "Text is edited.";
            Message editedMessage = await BotClient.EditMessageTextAsync(
                chatId: originalMessage.Chat.Id,
                messageId: originalMessage.MessageId,
                text: newText
            );

            Assert.Equal(newText, editedMessage.Text);
            Assert.Equal(originalMessage.MessageId, editedMessage.MessageId);
            // Assert.True(timeBeforeEdition < editedMessage.EditDate.Value); // ToDo: edit_date isn null. Check with @BotSupport 
        }

        [OrderedFact(DisplayName = FactTitles.ShouldEditInlineMessageText)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageText)]
        public async Task Should_Edit_Inline_Message_Text()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditInlineMessageText,
                startInlineQuery: true);

            #region Answer Inline Query with an Article

            Update inlineQUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            string data = "change-text" + new Random().Next(2_000);
            InlineKeyboardMarkup initialMarkup = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Click here to change text", data)
            });

            InlineQueryResultBase[] inlineQueryResults =
            {
                new InlineQueryResultArticle(
                    id: "bot-api",
                    title: "Telegram Bot API",
                    inputMessageContent: new InputTextMessageContent("https://core.telegram.org/bots/api")
                ) {ReplyMarkup = initialMarkup}
            };

            await BotClient.AnswerInlineQueryAsync(inlineQUpdate.InlineQuery.Id, inlineQueryResults, 0);

            #endregion

            Update callbackQUpdate = await _fixture.UpdateReceiver
                .GetCallbackQueryUpdateAsync(data: data);

            await BotClient.EditMessageTextAsync(
                inlineMessageId: callbackQUpdate.CallbackQuery.InlineMessageId,
                text: "✌ Edited 👌"
            );
        }

        [OrderedFact(DisplayName = FactTitles.ShouldEditMessageMarkup)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageReplyMarkup)]
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

        [OrderedFact(DisplayName = FactTitles.ShouldEditInlineMessageMarkup)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageReplyMarkup)]
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

            InputMessageContentBase inputMessageContent =
                new InputTextMessageContent("https://core.telegram.org/bots/api");

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
        }

        private static class FactTitles
        {
            public const string ShouldEditMessageText = "Should edit a message's text";

            public const string ShouldEditInlineMessageText = "Should edit an inline message's text";

            public const string ShouldEditMessageMarkup = "Should edit a message's markup";

            public const string ShouldEditInlineMessageMarkup = "Should edit an inline message's markup";
        }
    }
}