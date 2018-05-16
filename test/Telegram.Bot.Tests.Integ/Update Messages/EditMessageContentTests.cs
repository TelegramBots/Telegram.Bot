using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages
{
    [Collection(Constants.TestCollections.EditMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
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

            const string originalMessagePrefix = "original\n";
            (MessageEntityType Type, string Value)[] entityValueMappings = {
                (MessageEntityType.Bold, "<b>bold</b>"),
                (MessageEntityType.Italic, "<i>italic</i>"),
            };
            string messageText = originalMessagePrefix +
                    string.Join("\n", entityValueMappings.Select(tuple => tuple.Value));

            Message originalMessage = await BotClient.SendTextMessageAsync(
                chatId: _fixture.SupergroupChat.Id,
                text: messageText,
                parseMode: ParseMode.Html
            );

            DateTime timeBeforeEdition = DateTime.UtcNow;
            await Task.Delay(1_000);

            const string modifiedMessagePrefix = "modified\n";
            messageText = modifiedMessagePrefix +
                    string.Join("\n", entityValueMappings.Select(tuple => tuple.Value));
            Message editedMessage = await BotClient.EditMessageTextAsync(
                chatId: originalMessage.Chat.Id,
                messageId: originalMessage.MessageId,
                text: messageText,
                parseMode: ParseMode.Html
            );

            Assert.StartsWith(modifiedMessagePrefix, editedMessage.Text);
            Assert.Equal(originalMessage.MessageId, editedMessage.MessageId);
            Assert.True(timeBeforeEdition < editedMessage.EditDate);

            Assert.Equal(
                entityValueMappings.Select(tuple => tuple.Type),
                editedMessage.Entities.Select(e => e.Type)
            );
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

            const string originalMessagePrefix = "original\n";
            (MessageEntityType Type, string Value)[] entityValueMappings = {
                (MessageEntityType.Bold, "<b>bold</b>"),
                (MessageEntityType.Italic, "<i>italic</i>"),
            };
            string messageText = originalMessagePrefix +
                    string.Join("\n", entityValueMappings.Select(tuple => tuple.Value));
            string data = "change-text" + new Random().Next(2_000);

            InlineQueryResultBase[] inlineQueryResults =
            {
                new InlineQueryResultArticle(
                    id: "bot-api",
                    title: "Telegram Bot API",
                    inputMessageContent:
                        new InputTextMessageContent(messageText)
                        {
                            ParseMode = ParseMode.Html
                        }
                )
                {
                    ReplyMarkup = InlineKeyboardButton.WithCallbackData("Click here to modify text", data)
                }
            };

            await BotClient.AnswerInlineQueryAsync(inlineQUpdate.InlineQuery.Id, inlineQueryResults, 0);

            #endregion

            Update callbackQUpdate = await _fixture.UpdateReceiver
                .GetCallbackQueryUpdateAsync(data: data);

            const string modifiedMessagePrefix = "✌ modified 👌\n";
            messageText = modifiedMessagePrefix +
                    string.Join("\n", entityValueMappings.Select(tuple => tuple.Value));

            await BotClient.EditMessageTextAsync(
                inlineMessageId: callbackQUpdate.CallbackQuery.InlineMessageId,
                text: messageText,
                parseMode: ParseMode.Html
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

            DateTime timeBeforeEdition = DateTime.UtcNow;
            await Task.Delay(1_000);

            Message editedMessage = await BotClient.EditMessageReplyMarkupAsync(
                chatId: message.Chat.Id,
                messageId: message.MessageId,
                replyMarkup: "Edited 👍"
            );

            Assert.Equal(message.MessageId, editedMessage.MessageId);
            Assert.Equal(message.Text, editedMessage.Text);
            Assert.True(timeBeforeEdition < editedMessage.EditDate);
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

        [OrderedFact(DisplayName = FactTitles.ShouldEditMessageCaption)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageCaption)]
        public async Task Should_Edit_Message_Caption()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditMessageCaption);

            Message originalMessage;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Photos.Bot))
            {
                originalMessage = await BotClient.SendPhotoAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    photo: stream,
                    caption: "Message caption will be updated shortly"
                );
            }

            DateTime timeBeforeEdition = DateTime.UtcNow;
            await Task.Delay(1_000);

            const string newCaption = "Caption is edited.";

            Message editedMessage = await BotClient.EditMessageCaptionAsync(
                chatId: originalMessage.Chat.Id,
                messageId: originalMessage.MessageId,
                caption: newCaption
            );

            Assert.Equal(originalMessage.MessageId, editedMessage.MessageId);
            Assert.Equal(newCaption, editedMessage.Caption);
            Assert.True(timeBeforeEdition < editedMessage.EditDate);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldEditInlineMessageCaption)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageCaption)]
        public async Task Should_Edit_Inline_Message_Caption()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditInlineMessageCaption,
                startInlineQuery: true);

            #region Answer Inline Query with an Article

            Update inlineQUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            string data = "change-me" + new Random().Next(2_000);
            InlineKeyboardMarkup replyMarkup = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Click here to change caption", data)
            });
            const string url = "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg";

            InlineQueryResultBase[] inlineQueryResults =
            {
                new InlineQueryResultPhoto(
                    id: "photo1",
                    photoUrl: url,
                    thumbUrl: url
                )
                {
                    Caption = "Message caption will be updated shortly",
                    ReplyMarkup = replyMarkup
                }
            };

            await BotClient.AnswerInlineQueryAsync(inlineQUpdate.InlineQuery.Id, inlineQueryResults, 0);

            #endregion

            Update callbackQUpdate = await _fixture.UpdateReceiver
                .GetCallbackQueryUpdateAsync(data: data);

            await BotClient.EditMessageCaptionAsync(
                inlineMessageId: callbackQUpdate.CallbackQuery.InlineMessageId,
                caption: "Caption is edited 👌"
            );
        }

        private static class FactTitles
        {
            public const string ShouldEditMessageText = "Should edit a message's text";

            public const string ShouldEditInlineMessageText = "Should edit an inline message's text";

            public const string ShouldEditMessageMarkup = "Should edit a message's markup";

            public const string ShouldEditInlineMessageMarkup = "Should edit an inline message's markup";

            public const string ShouldEditMessageCaption = "Should edit a message's caption";

            public const string ShouldEditInlineMessageCaption = "Should edit an inline message's caption";
        }
    }
}
