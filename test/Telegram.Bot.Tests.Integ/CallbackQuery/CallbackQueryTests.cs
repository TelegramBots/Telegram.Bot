using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.CallbackQuery
{
    [Collection(CommonConstants.TestCollections.CallbackQuery)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class CallbackQueryTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public CallbackQueryTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldReceiveAnswerCallbackQuery)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendMessage)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.AnswerCallbackQuery)]
        [ExecutionOrder(1.1)]
        public async Task Should_Answer_With_Notification()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldReceiveAnswerCallbackQuery,
                "Click on *OK* button");

            const string callbackQueryData = "ok btn";
            var replyMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[]
            {
                new InlineKeyboardCallbackButton("OK", callbackQueryData),
            });

            Message message = await BotClient.SendTextMessageAsync(_fixture.SuperGroupChatId,
                "Please click on *OK* button.",
                ParseMode.Markdown,
                replyMarkup: replyMarkup);

            Update responseUpdate = await WaitForCallbackQueryUpdate(message.MessageId);

            bool result =
                await BotClient.AnswerCallbackQueryAsync(responseUpdate.CallbackQuery.Id, "You clicked on OK");

            Assert.Equal(UpdateType.CallbackQueryUpdate, responseUpdate.Type);
            Assert.Equal(message.MessageId, responseUpdate.CallbackQuery.Message.MessageId);
            Assert.Equal(callbackQueryData, responseUpdate.CallbackQuery.Data);
            Assert.True(result);
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerCallbackQueryWithAlert)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendMessage)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.AnswerCallbackQuery)]
        [ExecutionOrder(1.2)]
        public async Task Should_Answer_With_Alert()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldAnswerCallbackQueryWithAlert,
                "Click on *Notify* button");

            const string callbackQueryData = "Show Notification";
            var replyMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[]
            {
                new InlineKeyboardCallbackButton("Notify", callbackQueryData),
            });

            Message message = await BotClient.SendTextMessageAsync(_fixture.SuperGroupChatId,
                "Please click on *Notify* button.",
                ParseMode.Markdown,
                replyMarkup: replyMarkup);

            Update responseUpdate = await WaitForCallbackQueryUpdate(message.MessageId);

            bool result = await BotClient.AnswerCallbackQueryAsync(responseUpdate.CallbackQuery.Id, "Got it!", true);

            Assert.True(result);
        }

        [Obsolete]
        private async Task<Update> WaitForCallbackQueryUpdate(int messageId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Update update;

            var updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
                u => u.CallbackQuery.Message.MessageId == messageId,
                cancellationToken: cancellationToken,
                updateTypes: UpdateType.CallbackQueryUpdate);

            update = updates.Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

            return update;
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