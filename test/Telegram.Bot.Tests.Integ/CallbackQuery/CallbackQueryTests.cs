using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.CallbackQuery
{
    [Collection(Constants.TestCollections.CallbackQuery)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class CallbackQueryTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public CallbackQueryTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldReceiveAnswerCallbackQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerCallbackQuery)]
        [ExecutionOrder(1.1)]
        public async Task Should_Answer_With_Notification()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldReceiveAnswerCallbackQuery,
                "Click on *OK* button");

            string callbackQueryData = new Random().Next(5_000).ToString();
            IReplyMarkup replyMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[]
            {
                new InlineKeyboardCallbackButton("OK", callbackQueryData),
            });

            Message message = await BotClient.SendTextMessageAsync(_fixture.SuperGroupChatId,
                "Please click on *OK* button.",
                ParseMode.Markdown,
                replyMarkup: replyMarkup);

            Update responseUpdate = await _fixture.UpdateReceiver.GetCallbackQueryUpdateAsync(message.MessageId, false);

            bool result =
                await BotClient.AnswerCallbackQueryAsync(responseUpdate.CallbackQuery.Id, "You clicked on OK");

            Assert.Equal(UpdateType.CallbackQueryUpdate, responseUpdate.Type);
            Assert.Equal(message.MessageId, responseUpdate.CallbackQuery.Message.MessageId);
            Assert.Equal(callbackQueryData, responseUpdate.CallbackQuery.Data);
            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerCallbackQueryWithAlert)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerCallbackQuery)]
        [ExecutionOrder(1.2)]
        public async Task Should_Answer_With_Alert()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerCallbackQueryWithAlert,
                "Click on *Notify* button");

            string callbackQueryData = new Random().Next(5_000).ToString();
            IReplyMarkup replyMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[]
            {
                new InlineKeyboardCallbackButton("Notify", callbackQueryData),
            });

            Message message = await BotClient.SendTextMessageAsync(_fixture.SuperGroupChatId,
                "Please click on *Notify* button.",
                ParseMode.Markdown,
                replyMarkup: replyMarkup);

            Update responseUpdate = await _fixture.UpdateReceiver.GetCallbackQueryUpdateAsync(message.MessageId, false);

            bool result = await BotClient.AnswerCallbackQueryAsync(responseUpdate.CallbackQuery.Id, "Got it!", true);

            Assert.True(result);
            Assert.Equal(callbackQueryData, responseUpdate.CallbackQuery.Data);
        }

        private static class FactTitles
        {
            public const string ShouldReceiveAnswerCallbackQuery =
                "Should receive and answer callback query result with a notification";

            public const string ShouldAnswerCallbackQueryWithAlert =
                "Should receive and answer callback query result with an alert";
        }
    }
}