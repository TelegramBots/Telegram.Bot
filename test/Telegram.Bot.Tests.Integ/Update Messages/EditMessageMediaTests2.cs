using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Update_Messages
{
    [Collection(Constants.TestCollections.EditMessageMedia2)]
    [Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class EditMessageMediaTests2
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public EditMessageMediaTests2(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldEditMessageVideoWithFile)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.AnswerInlineQuery)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageMedia)]
        public async Task Should_Edit_Message_Video()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditMessageVideoWithFile);

            // Send a video to chat. This media will be changed later in test.
            Message originalMessage;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Animation.Earth))
            {
                originalMessage = await BotClient.SendVideoAsync(
                    chatId: _fixture.SupergroupChat,
                    video: stream,
                    caption: "This message will be edited shortly"
                );
            }

            await Task.Delay(500);

            // Replace vidoe with a document by uploading the new file
            Message editedMessage;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Certificate.PublicKey))
            {
                editedMessage = await BotClient.EditMessageMediaAsync(
                    originalMessage.Chat,
                    originalMessage.MessageId,
                    media: new InputMediaDocument
                    {
                        Media = new InputMedia(stream, "public-key.pem.txt"),
                        Caption = "**Public** key in `.pem` format",
                        ParseMode = ParseMode.Markdown,
                    }
                );
            }

            Assert.Equal(originalMessage.MessageId, editedMessage.MessageId);
            Assert.Equal(MessageType.Document, editedMessage.Type);
            Assert.NotNull(editedMessage.Document);
            Assert.Null(editedMessage.Video);
        }

        // ToDo Replace an audio with an animation. upload both animation file and its thumbnail

        private static class FactTitles
        {
            public const string ShouldEditMessageVideoWithFile =
                "Should change a message's video to a document file";
        }
    }
}
