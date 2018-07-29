using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
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
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendVideo)]
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
                    media: new InputMediaDocument(new InputMedia(stream, "public-key.pem.txt"))
                    {
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

        [OrderedFact(DisplayName = FactTitles.ShouldEditMessagePhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendPhoto)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.EditMessageMedia)]
        public async Task Should_Edit_Message_Photo()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldEditMessagePhoto);

            // Upload a GIF file to Telegram servers and obtain its file_id. This file_id will be used later in test.
            Message gifMessage = await BotClient.SendDocumentAsync(
                chatId: _fixture.SupergroupChat,
                document: "https://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif",
                caption: "`file_id` of this GIF will be used"
            );

            // Send a photo to chat. This media will be changed later in test.
            Message originalMessage = await BotClient.SendPhotoAsync(
                chatId: _fixture.SupergroupChat,
                photo: "https://cdn.pixabay.com/photo/2017/08/30/12/45/girl-2696947_640.jpg",
                caption: "This message will be edited shortly"
            );

            await Task.Delay(500);

            // Replace audio with another audio by uploading the new file. A thumbnail image is also uploaded.
            Message editedMessage;
            using (Stream thumbStream = System.IO.File.OpenRead(Constants.FileNames.Thumbnail.Video))
            {
                editedMessage = await BotClient.EditMessageMediaAsync(
                    originalMessage.Chat,
                    originalMessage.MessageId,
                    media: new InputMediaAnimation(gifMessage.Document.FileId)
                    {
                        Thumb = new InputMedia(thumbStream, "thumb.jpg"),
                        Duration = 4,
                        Height = 320,
                        Width = 320,
                    }
                );
            }

            Assert.Equal(originalMessage.MessageId, editedMessage.MessageId);

            // For backward compatibility, when this field is set, the document field will also be set.
            // In that case, message type is still considered as Document.
            Assert.Equal(MessageType.Document, editedMessage.Type);
            Assert.NotNull(editedMessage.Document);
            Assert.NotNull(editedMessage.Animation);

            Assert.NotEqual(0, editedMessage.Animation.Duration);
            Assert.NotEqual(0, editedMessage.Animation.Width);
            Assert.NotEqual(0, editedMessage.Animation.Height);
            Assert.NotEqual(0, editedMessage.Animation.FileSize);
            Assert.NotEmpty(editedMessage.Animation.FileId);
            Assert.NotEmpty(editedMessage.Animation.FileName);
            Assert.NotEmpty(editedMessage.Animation.MimeType);

            Assert.NotNull(editedMessage.Animation.Thumb);
            Assert.NotEmpty(editedMessage.Animation.Thumb.FileId);
            Assert.True(JToken.DeepEquals(
                JObject.FromObject(editedMessage.Animation.Thumb),
                JObject.FromObject(editedMessage.Document.Thumb)
            ));
        }

        private static class FactTitles
        {
            public const string ShouldEditMessageVideoWithFile =
                "Should change a message's video to a document file";

            public const string ShouldEditMessagePhoto =
                "Should change a message's photo to an animation having thumbnail";
        }
    }
}
