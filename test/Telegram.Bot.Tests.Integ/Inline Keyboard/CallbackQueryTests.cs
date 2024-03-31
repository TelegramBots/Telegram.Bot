using System;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
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
public class CallbackQueryTests(TestsFixture fixture)
{
    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should receive and answer callback query result with a notification")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerCallbackQuery)]
    public async Task Should_Answer_With_Notification()
    {
        string callbackQueryData = 'a' + new Random().Next(5_000).ToString();

        Message message = await BotClient.SendMessageAsync(
            new()
            {
                ChatId = fixture.SupergroupChat.Id,
                Text = "Please click on *OK* button.",
                ParseMode = ParseMode.Markdown,
                ReplyMarkup = new InlineKeyboardMarkup([InlineKeyboardButton.WithCallbackData("OK", callbackQueryData)]),
            }
        );

        Update responseUpdate = await fixture.UpdateReceiver.GetCallbackQueryUpdateAsync(message.MessageId);
        CallbackQuery callbackQuery = responseUpdate.CallbackQuery!;

        await BotClient.AnswerCallbackQueryAsync(
            new AnswerCallbackQueryRequest
            {
                CallbackQueryId = callbackQuery!.Id,
                Text = "You clicked on OK",
            }
        );

        Assert.Equal(UpdateType.CallbackQuery, responseUpdate.Type);
        Assert.Equal(callbackQueryData, callbackQuery.Data);
        Assert.NotEmpty(callbackQuery.ChatInstance);
        Assert.Null(callbackQuery.InlineMessageId);
        Assert.Null(callbackQuery.GameShortName);
        Assert.False(callbackQuery.IsGameQuery);
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

        Message message = await BotClient.SendMessageAsync(
            new()
            {
                ChatId = fixture.SupergroupChat.Id,
                Text = "Please click on *Notify* button.",
                ParseMode = ParseMode.Markdown,
                ReplyMarkup = new InlineKeyboardMarkup(
                    InlineKeyboardButton.WithCallbackData("Notify", callbackQueryData)
                ),
            }
        );

        Update responseUpdate = await fixture.UpdateReceiver.GetCallbackQueryUpdateAsync(message.MessageId);
        CallbackQuery callbackQuery = responseUpdate.CallbackQuery!;

        await BotClient.AnswerCallbackQueryAsync(
            new AnswerCallbackQueryRequest
            {
                CallbackQueryId = responseUpdate.CallbackQuery!.Id,
                Text = "Got it!",
                ShowAlert = true,
            }
        );

        Assert.Equal(UpdateType.CallbackQuery, responseUpdate.Type);
        Assert.Equal(callbackQueryData, callbackQuery.Data);
        Assert.NotEmpty(callbackQuery.ChatInstance);
        Assert.Null(callbackQuery.InlineMessageId);
        Assert.Null(callbackQuery.GameShortName);
        Assert.False(callbackQuery.IsGameQuery);
        Assert.False(callbackQuery.From.IsBot);
        Assert.NotNull(callbackQuery.From.Username);
        Assert.NotEmpty(callbackQuery.From.Username);
        Assert.NotNull(callbackQuery.Message);
        Asserts.JsonEquals(message, callbackQuery.Message);
    }
}
