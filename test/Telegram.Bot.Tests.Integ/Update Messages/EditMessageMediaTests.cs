using System;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
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

            #region Answer Inline Query with a Photo

            Update inlineQUpdate = await _fixture.UpdateReceiver.GetInlineQueryUpdateAsync();

            string data = "change-photo:" + new Random().Next(2_000);

            InlineQueryResultBase[] inlineQueryResults =
            {
                new InlineQueryResultPhoto(
                    id: "photo:rainbow-girl",
                    photoUrl: "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg",
                    thumbUrl: "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg"
                )
                {
                    Caption = "Rainbow Girl",
                    ReplyMarkup = InlineKeyboardButton.WithCallbackData("Click here to change this photo", data),
                }
            };

            await BotClient.AnswerInlineQueryAsync(inlineQUpdate.InlineQuery.Id, inlineQueryResults, 0);

            #endregion

            // Bot waits for user to click on inline button under the photo
            Update cqUpdate = await _fixture.UpdateReceiver.GetCallbackQueryUpdateAsync(data: data);

            // Change the photo. Note that, in the case of an inline message, the new photo should be either an URL or
            // the file_id of a previously uploaded photo.
            await BotClient.EditMessageMediaAsync(
                inlineMessageId: cqUpdate.CallbackQuery.InlineMessageId,
                media: new InputMediaPhoto
                {
                    Media = "https://cdn.pixabay.com/photo/2017/04/11/21/34/giraffe-2222908_640.jpg",
                    Caption = "**Giraffe**",
                    ParseMode = ParseMode.Markdown,
                }
            );
        }

        private static class FactTitles
        {
            public const string ShouldEditInlineMessagePhotoWithUrl =
                "Should change an inline message's photo using a photo URL";
        }
    }
}
