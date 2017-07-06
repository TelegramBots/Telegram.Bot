using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.CallbackQuery
{
    [Collection(CommonConstants.TestCollectionName)]
    [Trait(CommonConstants.CategoryTraitName, CommonConstants.TestCategories.CallbackQueries)]
    public class CallbackQueryTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly BotClientFixture _fixture;

        public CallbackQueryTests(BotClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = FactTitles.ShouldReceiveCallbackQuery)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendMessage)]
        public async Task ShouldReceiveCallbackQuery()
        {
            await _fixture.SendTestCaseNotification(FactTitles.ShouldReceiveCallbackQuery,
                "Click on *OK* button");

            const string callbackQueryData = "ok btn";
            var replyMarkup = new InlineKeyboardMarkup(new[]
            {
                new InlineKeyboardButton("OK", callbackQueryData),
            });

            Message message = await BotClient.SendTextMessageAsync(_fixture.ChatId,
                "Please click on *OK* button.",
                ParseMode.Markdown,
                replyMarkup: replyMarkup);

            Update responseUpdate = await WaitForCallbackQueryUpdate(message.MessageId);

            Assert.Equal(UpdateType.CallbackQueryUpdate, responseUpdate.Type);
            Assert.Equal(message.MessageId, responseUpdate.CallbackQuery.Message.MessageId);
            Assert.Equal(callbackQueryData, responseUpdate.CallbackQuery.Data);
        }

        [Fact(DisplayName = FactTitles.ShouldAnswerCallbackQuery)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendMessage)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.AnswerCallbackQuery)]
        public async Task ShouldAnswerCallbackQuery()
        {
            await _fixture.SendTestCaseNotification(FactTitles.ShouldAnswerCallbackQuery,
                "Click on *Notify* button");

            const string callbackQueryData = "Show Notification";
            var replyMarkup = new InlineKeyboardMarkup(new[]
            {
                new InlineKeyboardButton("Notify", callbackQueryData),
                new InlineKeyboardButton("cancel", "Cancel"),
            });

            Message message = await BotClient.SendTextMessageAsync(_fixture.ChatId,
                "Please click on *Notify* button.",
                ParseMode.Markdown,
                replyMarkup: replyMarkup);

            Update responseUpdate = await WaitForCallbackQueryUpdate(message.MessageId);

            bool result = await BotClient.AnswerCallbackQueryAsync(
                responseUpdate.CallbackQuery.Id,
                "Got it!");

            Assert.True(result);
        }


        private async Task<Update> WaitForCallbackQueryUpdate(int messageId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var updates = await _fixture.UpdateReceiver.GetUpdatesAsync(u => u.CallbackQuery.Message.MessageId == messageId,
                cancellationToken: cancellationToken,
                updateTypes: UpdateType.CallbackQueryUpdate);

            var update = updates.Single();

            await _fixture.UpdateReceiver.DiscardNewUpdatesAsync(cancellationToken);

            return update;
        }

        private static class FactTitles
        {
            public const string ShouldReceiveCallbackQuery = "Should send a message with inline buttons and receive callback query result";

            public const string ShouldAnswerCallbackQuery = "Should send a message with inline buttons and answer to its callback query with a notification";
        }
    }
}
