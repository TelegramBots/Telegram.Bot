using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.UpdatingMessages
{
    [Collection(Constants.TestCollections.UpdateMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class UpdateMessageTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public UpdateMessageTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        #region 1. Updating messages sent by the bot

        [Fact(DisplayName = FactTitles.ShouldEditMessageMarkup)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageReplyMarkup)]
        [ExecutionOrder(1.3)]
        public async Task Should_Edit_Message_Markup()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditMessageMarkup);

            var initialMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[]
            {
                new InlineKeyboardCallbackButton("Original markup", "data")
            });
            var editedMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[]
            {
                new InlineKeyboardCallbackButton("Keyboard button edited", "editedData")
            });

            var message = await BotClient.SendTextMessageAsync(_fixture.SuperGroupChatId,
                "Inline keyboard will be updated shortly", replyMarkup: initialMarkup);

            await Task.Delay(500);

            var editedMessage =
                await BotClient.EditMessageReplyMarkupAsync(message.Chat.Id, message.MessageId, editedMarkup);

            Assert.Equal(message.MessageId, editedMessage.MessageId);
        }

        // ToDo: edit text
        // ToDo: Remove markup
        // ToDo: edit/remove caption
        // ToDo: delete message

        #endregion

        #region 2. Updating inline messages(sent via the bot)

        [Fact(DisplayName = FactTitles.ShouldEditInlineMessageMarkup)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageReplyMarkup)]
        [ExecutionOrder(2.1)]
        public async Task Should_Edit_Inline_Message_Markup()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditInlineMessageMarkup,
                "Start an inline query, post its result to chat, and click on the inline button");

            string data = "change-me" + new Random().Next(2_000);
            var initialMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[]
            {
                new InlineKeyboardCallbackButton("Click here to change this button", data)
            });
            var editedMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[]
            {
                new InlineKeyboardCallbackButton("Keyboard button edited", "edited")
            });
            var inlineQueryResults = new InlineQueryResult[]
            {
                new InlineQueryResultArticle
                {
                    Id = "bot-api",
                    Title = "Telegram Bot API",
                    Description = "The Bot API is an HTTP-based interface created for developers",
                    InputMessageContent = new InputTextMessageContent
                    {
                        MessageText = "https://core.telegram.org/bots/api"
                    },
                    ReplyMarkup = initialMarkup,
                },
            };
            var iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();
            await BotClient.AnswerInlineQueryAsync(iqUpdate.InlineQuery.Id, inlineQueryResults, 0);
            var callbackUpdate = await _fixture.UpdateReceiver.GetCallbackQueryUpdateAsync();
            bool result = await BotClient.EditInlineMessageReplyMarkupAsync(
                callbackUpdate.CallbackQuery.InlineMessageId,
                editedMarkup);

            Assert.Equal(data, callbackUpdate.CallbackQuery.Data);
            Assert.True(result);
        }

        #endregion

        private static class FactTitles
        {
            public const string ShouldEditMessageMarkup = "Should edit a message's markup";

            public const string ShouldEditInlineMessageMarkup = "Should edit an inline message's markup";
        }
    }
}