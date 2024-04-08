using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages;

[Collection(Constants.TestCollections.EditMessage)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class EditMessageContentTests(TestsFixture fixture)
{
    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should edit an inline message's text")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageText)]
    public async Task Should_Edit_Inline_Message_Text()
    {
        await fixture.SendTestInstructionsAsync(
            "Starting the inline query with this message...",
            startInlineQuery: true
        );

        #region Answer Inline Query with an Article

        Update inlineQUpdate = await fixture.UpdateReceiver.GetInlineQueryUpdateAsync();
        Assert.NotNull(inlineQUpdate.InlineQuery);

        const string originalMessagePrefix = "original\n";
        (MessageEntityType Type, string Value)[] entityValueMappings =
        [
            (MessageEntityType.Bold, "<b>bold</b>"),
            (MessageEntityType.Italic, "<i>italic</i>")
        ];
        string messageText = $"{originalMessagePrefix}{string.Join("\n", entityValueMappings.Select(tuple => tuple.Value))}";
        string data = $"change-text{new Random().Next(2_000)}";

        InlineQueryResult[] inlineQueryResults =
        [
            new InlineQueryResultArticle
            {
                Id = "bot-api",
                Title = "Telegram Bot API",
                InputMessageContent = new InputTextMessageContent
                {
                    MessageText = messageText,
                    ParseMode = ParseMode.Html
                },
                ReplyMarkup = InlineKeyboardButton.WithCallbackData("Click here to modify text", data)
            }
        ];

        await BotClient.AnswerInlineQueryAsync(
            new()
            {
                InlineQueryId = inlineQUpdate.InlineQuery.Id,
                Results = inlineQueryResults,
                CacheTime = 0,
            }
        );

        #endregion

        Update callbackQUpdate = await fixture.UpdateReceiver
            .GetCallbackQueryUpdateAsync(data: data);

        Assert.NotNull(callbackQUpdate.CallbackQuery);
        Assert.NotNull(callbackQUpdate.CallbackQuery.InlineMessageId);

        const string modifiedMessagePrefix = "✌ modified 👌\n";
        messageText = $"{modifiedMessagePrefix}{string.Join("\n", entityValueMappings.Select(tuple => tuple.Value))}";

        await BotClient.EditInlineMessageTextAsync(
            new()
            {
                InlineMessageId = callbackQUpdate.CallbackQuery.InlineMessageId,
                Text = messageText,
                ParseMode = ParseMode.Html,
            }
        );
    }

    [OrderedFact("Should edit an inline message's markup")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageReplyMarkup)]
    public async Task Should_Edit_Inline_Message_Markup()
    {
        await fixture.SendTestInstructionsAsync(
            "Starting the inline query with this message...",
            startInlineQuery: true
        );

        #region Answer Inline Query with an Article

        Update inlineQUpdate = await fixture.UpdateReceiver.GetInlineQueryUpdateAsync();
        Assert.NotNull(inlineQUpdate.InlineQuery);

        string data = "change-me" + new Random().Next(2_000);
        InlineKeyboardMarkup initialMarkup = new(new[]
        {
            InlineKeyboardButton.WithCallbackData("Click here to change this button", data)
        });

        InputMessageContent inputMessageContent = new InputTextMessageContent
        {
            MessageText = "https://core.telegram.org/bots/api"
        };

        InlineQueryResult[] inlineQueryResults =
        [
            new InlineQueryResultArticle
            {
                Id = "bot-api",
                Title = "Telegram Bot API",
                InputMessageContent = inputMessageContent,
                Description = "The Bot API is an HTTP-based interface created for developers",
                ReplyMarkup = initialMarkup,
            }
        ];

        await BotClient.AnswerInlineQueryAsync(
            new()
            {
                InlineQueryId = inlineQUpdate.InlineQuery.Id,
                Results = inlineQueryResults,
                CacheTime = 0,
            }
        );

        #endregion

        Update callbackQUpdate = await fixture.UpdateReceiver
            .GetCallbackQueryUpdateAsync(data: data);

        Assert.NotNull(callbackQUpdate.CallbackQuery);
        Assert.NotNull(callbackQUpdate.CallbackQuery.InlineMessageId);

        await BotClient.EditInlineMessageReplyMarkupAsync(
            new()
            {
                InlineMessageId = callbackQUpdate.CallbackQuery.InlineMessageId,
                ReplyMarkup = "✌ Edited 👌",
            }
        );
    }

    [OrderedFact("Should edit an inline message's caption")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageCaption)]
    public async Task Should_Edit_Inline_Message_Caption()
    {
        await fixture.SendTestInstructionsAsync(
            "Starting the inline query with this message...",
            startInlineQuery: true
        );

        #region Answer Inline Query with an Article

        Update inlineQUpdate = await fixture.UpdateReceiver.GetInlineQueryUpdateAsync();
        Assert.NotNull(inlineQUpdate.InlineQuery);

        string data = "change-me" + new Random().Next(2_000);
        InlineKeyboardMarkup replyMarkup = new(new[]
        {
            InlineKeyboardButton.WithCallbackData("Click here to change caption", data)
        });
        const string url = "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg";

        InlineQueryResult[] inlineQueryResults =
        [
            new InlineQueryResultPhoto
            {
                Id = "photo1",
                PhotoUrl = url,
                ThumbnailUrl = url,
                Caption = "Message caption will be updated shortly",
                ReplyMarkup = replyMarkup
            }
        ];

        await BotClient.AnswerInlineQueryAsync(
            new()
            {
                InlineQueryId = inlineQUpdate.InlineQuery.Id,
                Results = inlineQueryResults,
                CacheTime = 0,
            }
        );

        #endregion

        Update callbackQUpdate = await fixture.UpdateReceiver
            .GetCallbackQueryUpdateAsync(data: data);

        Assert.NotNull(callbackQUpdate.CallbackQuery);
        Assert.NotNull(callbackQUpdate.CallbackQuery.InlineMessageId);

        await BotClient.EditInlineMessageCaptionAsync(
            new()
            {
                InlineMessageId = callbackQUpdate.CallbackQuery.InlineMessageId,
                Caption = "_Caption is edited_ 👌",
                ParseMode = ParseMode.Markdown,
            }
        );
    }
}
