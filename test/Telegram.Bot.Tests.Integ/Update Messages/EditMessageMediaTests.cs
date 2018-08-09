using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages
{
    [Collection(Constants.TestCollections.EditMessageMedia)]
    [Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class EditMessageMediaTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public EditMessageMediaTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldEditInlineMessagePhotoWithUrl)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageMedia)]
        public async Task Should_Edit_Inline_Message_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditInlineMessagePhotoWithUrl,
                startInlineQuery: true);

            #region Answer Inline Query with a media message

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] inlineQueryResults =
            {
                new InlineQueryResultPhoto(
                    id: "photo:rainbow-girl",
                    photoUrl: "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg",
                    thumbUrl: "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg"
                )
                {
                    Caption = "Rainbow Girl",
                    ReplyMarkup = InlineKeyboardButton.WithCallbackData("Click here to edit"),
                }
            };

            await BotClient.AnswerInlineQueryAsync(iqUpdate.InlineQuery.Id, inlineQueryResults, 0);

            #endregion

            // Bot waits for user to click on inline button under the media
            Update cqUpdate = await _fixture.UpdateReceiver.GetCallbackQueryUpdateAsync(data: "Click here to edit");

            // Change the photo for an audio. Note that, in the case of an inline message, the new media should be
            // either an URL or the file_id of a previously uploaded media.
            await BotClient.EditMessageMediaAsync(
                inlineMessageId: cqUpdate.CallbackQuery.InlineMessageId,
                media: new InputMediaAudio(
                    "https://upload.wikimedia.org/wikipedia/commons/transcoded/b/bb/" +
                    "Test_ogg_mp3_48kbps.wav/Test_ogg_mp3_48kbps.wav.mp3"
                )
                {
                    Caption = "**Audio** in `.mp3` format",
                    ParseMode = ParseMode.Markdown,
                }
            );
        }

        [OrderedFact(DisplayName = FactTitles.ShouldEditInlineMessageDocumentWithFileId)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageMedia)]
        public async Task ShouldEditInlineMessageDocumentWithFileId()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditInlineMessageDocumentWithFileId);

            // Upload a GIF file to Telegram servers and obtain its file_id. This file_id will be used later in test.
            string animationFileId;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Animation.Earth))
            {
                Message gifMessage = await BotClient.SendDocumentAsync(
                    chatId: _fixture.SupergroupChat,
                    document: new InputOnlineFile(stream, "Earth.gif"),
                    caption: "`file_id` of this GIF will be used",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton
                        .WithSwitchInlineQueryCurrentChat("Start Inline Query")
                );
                animationFileId = gifMessage.Document.FileId;
            }

            #region Answer Inline Query with a media message

            Update iqUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            InlineQueryResultBase[] inlineQueryResults =
            {
                new InlineQueryResultDocument(
                    id: "document:acrobat",
                    documentUrl: "http://www.adobe.com/content/dam/acom/en/devnet/acrobat/pdfs/pdf_open_parameters.pdf",
                    title: "Parameters for Opening PDF Files",
                    mimeType: "application/pdf"
                )
                {
                    ReplyMarkup = InlineKeyboardButton.WithCallbackData("Click here to edit"),
                }
            };

            await BotClient.AnswerInlineQueryAsync(iqUpdate.InlineQuery.Id, inlineQueryResults, 0);

            #endregion

            // Bot waits for user to click on inline button under the media
            Update cqUpdate = await _fixture.UpdateReceiver.GetCallbackQueryUpdateAsync(data: "Click here to edit");

            // Change the YouTube video for an animation. Note that, in the case of an inline message, the new media
            // should be either an URL or the file_id of a previously uploaded media.
            // Also, animation thumbnail cannot be uploaded for an inline message.
            await BotClient.EditMessageMediaAsync(
                inlineMessageId: cqUpdate.CallbackQuery.InlineMessageId,
                media: new InputMediaAnimation(animationFileId)
            );
        }

        private static class FactTitles
        {
            public const string ShouldEditInlineMessagePhotoWithUrl =
                "Should change an inline message's photo to an audio using URL";

            public const string ShouldEditInlineMessageDocumentWithFileId =
                "Should change an inline message's document to an animation using file_id";
        }
    }
}
