using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Tests.Integ.Interactive;

[Collection(Constants.TestCollections.CallbackQuery)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class CallbackQueryTests(TestsFixture fixture) : TestClass(fixture)
{
    [OrderedFact("Should receive and answer callback query result with a notification")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerCallbackQuery)]
    public async Task Should_Answer_With_Notification()
    {
        string callbackQueryData = 'a' + new Random().Next(5_000).ToString();

        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: "Please click on *OK* button.",
            parseMode: ParseMode.Markdown,
            replyMarkup: new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("OK", callbackQueryData)
            })
        );

        Update responseUpdate = await Fixture.UpdateReceiver.GetCallbackQueryUpdateAsync(message.Id);
        CallbackQuery callbackQuery = responseUpdate.CallbackQuery!;

        await BotClient.AnswerCallbackQuery(
            callbackQueryId: callbackQuery!.Id,
            text: "You clicked on OK"
        );

        Assert.Equal(UpdateType.CallbackQuery, responseUpdate.Type);
        Assert.Equal(callbackQueryData, callbackQuery.Data);
        Assert.NotEmpty(callbackQuery.ChatInstance);
        Assert.Null(callbackQuery.InlineMessageId);
        Assert.Null(callbackQuery.GameShortName);
        Assert.False(callbackQuery.From.IsBot);
        Assert.NotNull(callbackQuery.From.Username);
        Assert.NotEmpty(callbackQuery.From.Username);
        Asserts.JsonEquals(message, callbackQuery.Message);
    }

    [OrderedFact("Should receive and answer callback query result with an alert")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerCallbackQuery)]
    public async Task Should_Answer_With_Alert()
    {
        string callbackQueryData = $"b{new Random().Next(5_000)}";

        Message message = await BotClient.SendMessage(
            chatId: Fixture.SupergroupChat.Id,
            text: "Please click on *Notify* button.",
            parseMode: ParseMode.Markdown,
            replyMarkup: new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Notify", callbackQueryData)
            )
        );

        Update responseUpdate = await Fixture.UpdateReceiver.GetCallbackQueryUpdateAsync(message.Id);
        CallbackQuery callbackQuery = responseUpdate.CallbackQuery!;

        await BotClient.AnswerCallbackQuery(
            callbackQueryId: responseUpdate.CallbackQuery!.Id,
            text: "Got it!",
            showAlert: true
        );

        Assert.Equal(UpdateType.CallbackQuery, responseUpdate.Type);
        Assert.Equal(callbackQueryData, callbackQuery.Data);
        Assert.NotEmpty(callbackQuery.ChatInstance);
        Assert.Null(callbackQuery.InlineMessageId);
        Assert.Null(callbackQuery.GameShortName);
        Assert.False(callbackQuery.From.IsBot);
        Assert.NotNull(callbackQuery.From.Username);
        Assert.NotEmpty(callbackQuery.From.Username);
        Assert.NotNull(callbackQuery.Message);
        Asserts.JsonEquals(message, callbackQuery.Message);
    }
}
