using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages;

[Collection(Constants.TestCollections.EditMessageMedia)]
[Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class EditMessageMediaTests(TestsFixture fixture)
{
    ITelegramBotClient BotClient => fixture.BotClient;

    [OrderedFact("Should change an inline message's photo to an audio using URL")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageMedia)]
    public async Task Should_Edit_Inline_Message_Photo()
    {
        await fixture.SendTestInstructionsAsync(
            "Starting the inline query with this message...",
            startInlineQuery: true
        );

        #region Answer Inline Query with a media message

        Update iqUpdate = await fixture.UpdateReceiver.GetInlineQueryUpdateAsync();
        Assert.NotNull(iqUpdate.InlineQuery);

        InlineQueryResult[] inlineQueryResults =
        [
            new InlineQueryResultPhoto
            {
                Id = "photo:rainbow-girl",
                PhotoUrl = "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg",
                ThumbnailUrl = "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg",
                Caption = "Rainbow Girl",
                ReplyMarkup = InlineKeyboardButton.WithCallbackData("Click here to edit"),
            }
        ];

        await BotClient.AnswerInlineQueryAsync(
            new()
            {
                InlineQueryId = iqUpdate.InlineQuery.Id,
                Results = inlineQueryResults,
                CacheTime = 0,
            }
        );

        #endregion

        // Bot waits for user to click on inline button under the media
        Update cqUpdate = await fixture.UpdateReceiver.GetCallbackQueryUpdateAsync(data: "Click here to edit");

        Assert.NotNull(cqUpdate.CallbackQuery);
        Assert.NotNull(cqUpdate.CallbackQuery.InlineMessageId);

        // Change the photo for an audio. Note that, in the case of an inline message, the new media should be
        // either an URL or the file_id of a previously uploaded media.
        InputFileUrl inputFileUrl = InputFile.FromUri("https://upload.wikimedia.org/wikipedia/commons/transcoded/b/bb/Test_ogg_mp3_48kbps.wav/Test_ogg_mp3_48kbps.wav.mp3");
        await BotClient.EditInlineMessageMediaAsync(
            new()
            {
                InlineMessageId = cqUpdate.CallbackQuery.InlineMessageId,
                Media = new InputMediaAudio
                {
                    Media = inputFileUrl,
                    Caption = "**Audio** in `.mp3` format",
                    ParseMode = ParseMode.Markdown,
                },
            }
        );
    }

    [OrderedFact("Should change an inline message's document to an animation using file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageMedia)]
    public async Task Should_Edit_Inline_Message_Document_With_FileId()
    {
        // Upload a GIF file to Telegram servers and obtain its file_id. This file_id will be used later in test.
        await using Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Animation.Earth);
        Message gifMessage = await BotClient.SendDocumentAsync(
            new()
            {
                ChatId = fixture.SupergroupChat,
                Document = InputFile.FromStream(stream, "Earth.gif"),
                Caption = "`file_id` of this GIF will be used",
                ParseMode = ParseMode.Markdown,
                ReplyMarkup = (InlineKeyboardMarkup) InlineKeyboardButton
                    .WithSwitchInlineQueryCurrentChat("Start Inline Query"),
            }
        );

        Assert.NotNull(gifMessage.Document);
        string animationFileId = gifMessage.Document.FileId;

        #region Answer Inline Query with a media message

        Update iqUpdate = await fixture.UpdateReceiver.GetInlineQueryUpdateAsync();
        Assert.NotNull(iqUpdate.InlineQuery);

        InlineQueryResult[] inlineQueryResults =
        [
            new InlineQueryResultDocument
            {
                Id = "document:acrobat",
                DocumentUrl = "https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf",
                Title = "Dummy PDF File",
                MimeType = "application/pdf",
                ReplyMarkup = InlineKeyboardButton.WithCallbackData("Click here to edit"),
            }
        ];

        await BotClient.AnswerInlineQueryAsync(
            new()
            {
                InlineQueryId = iqUpdate.InlineQuery.Id,
                Results = inlineQueryResults,
                CacheTime = 0,
            }
        );

        #endregion

        // Bot waits for user to click on inline button under the media
        Update cqUpdate = await fixture.UpdateReceiver.GetCallbackQueryUpdateAsync(data: "Click here to edit");
        Assert.NotNull(cqUpdate.CallbackQuery);
        Assert.NotNull(cqUpdate.CallbackQuery.InlineMessageId);

        // Change the YouTube video for an animation. Note that, in the case of an inline message, the new media
        // should be either an URL or the file_id of a previously uploaded media.
        // Also, animation thumbnail cannot be uploaded for an inline message.
        await BotClient.EditInlineMessageMediaAsync(
            new()
            {
                InlineMessageId =  cqUpdate.CallbackQuery.InlineMessageId,
                Media =  new InputMediaAnimation { Media = InputFile.FromFileId(animationFileId) },
            }
        );
    }
}
