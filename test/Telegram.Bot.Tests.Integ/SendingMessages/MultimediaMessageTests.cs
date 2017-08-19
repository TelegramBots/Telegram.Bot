using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.SendingMessages
{
    [Collection(CommonConstants.TestCollections.MultimediaMessage)]
    [TestCaseOrderer(CommonConstants.TestCaseOrderer, CommonConstants.AssemblyName)]
    public class MultimediaMessageTests
    {
        public ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public MultimediaMessageTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        #region 1. Sending photos

        // ToDo: Should Send Photo. Add its file_id to class fixture to be used in the next test

        [Fact(DisplayName = FactTitles.ShouldSendPhotoUsingFileId)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendPhoto)]
        [ExecutionOrder(1.1)]
        public async Task Should_Send_Photo_Using_FileId()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendPhotoUsingFileId);

            Message message, message2;

            using (var stream = new FileStream("Files/Photo/t_logo.png", FileMode.Open))
            {
                message = await BotClient.SendPhotoAsync(_fixture.SuperGroupChatId, stream.ToFileToSend("logo.png"),
                    "👆 This is:\nTelegram's Logo");
            }
            message2 = await BotClient.SendPhotoAsync(_fixture.SuperGroupChatId,
                new FileToSend(message.Photo.First().FileId));

            Assert.Equal(message.Photo.Select(ps => ps.FileId), message2.Photo.Select(ps => ps.FileId));

            _fixture.GiveMessageToNextTest = message;
            // ToDo: Add more asserts
        }

        [Fact(DisplayName = FactTitles.ShouldSerializeAndDeserializePhotoMessage)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.JsonConvert)]
        [ExecutionOrder(1.2)]
        public async Task ShouldSerializeAndDeserializePhotoMessage()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSerializeAndDeserializePhotoMessage);

            Message message = _fixture.GiveMessageToNextTest;
            string json = JsonConvert.SerializeObject(message);
            Message message2 = JsonConvert.DeserializeObject<Message>(json);

            Assert.Equal(message.Photo.Select(ps => ps.FileId), message2.Photo.Select(ps => ps.FileId));
        }

        #endregion

        #region 2. Sending videos

        [Fact(DisplayName = FactTitles.ShouldSendVideo)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendVideo)]
        [ExecutionOrder(2.1)]
        public async Task Should_Send_Video()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendVideo);

            const int duration = 104;
            const int width = 320;
            const int height = 240;
            const string caption = "Moon Landing";
            const string mimeType = "video/mp4";

            Message message;
            using (var stream = new FileStream("Files/Video/moon-landing.mp4", FileMode.Open))
            {
                message = await BotClient.SendVideoAsync(_fixture.SuperGroupChatId,
                    new FileToSend("moon-landing.mp4", stream), duration, width, height, caption);
            }

            Assert.Equal(MessageType.VideoMessage, message.Type);
            Assert.Equal(duration, message.Video.Duration);
            Assert.Equal(width, message.Video.Width);
            Assert.Equal(height, message.Video.Height);
            Assert.Equal(mimeType, message.Video.MimeType);
            Assert.NotNull(message.Video.Thumb);

            _fixture.GiveMessageToNextTest = message;
        }

        [Fact(DisplayName = FactTitles.ShouldSerializeAndDeserializeVideoMessage)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.JsonConvert)]
        [ExecutionOrder(2.2)]
        public async Task ShouldSerializeAndDeserializeVideoMessage()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSerializeAndDeserializeVideoMessage);

            Message message = _fixture.GiveMessageToNextTest;
            string json = JsonConvert.SerializeObject(message);
            Message message2 = JsonConvert.DeserializeObject<Message>(json);

            Assert.Equal(MessageType.VideoMessage, message2.Type);
            Assert.Equal(message.Video.Duration, message2.Video.Duration);
            Assert.Equal(message.Video.Width, message2.Video.Width);
            Assert.Equal(message.Video.Height, message2.Video.Height);
            Assert.Equal(message.Video.MimeType, message2.Video.MimeType);
            Assert.Equal(message.Video.Thumb, message2.Video.Thumb);
        }

        [Fact(DisplayName = FactTitles.ShouldSendVideoNote)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendVideoNote)]
        [ExecutionOrder(2.3)]
        public async Task Should_Send_Video_Note()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendVideoNote);

            const int duration = 28;
            const int widthAndHeight = 240;

            Message message;
            using (var stream = new FileStream("Files/Video/golden-ratio-240px.mp4", FileMode.Open))
            {
                message = await BotClient.SendVideoNoteAsync(_fixture.SuperGroupChatId,
                    new FileToSend("golden-ratio.mp4", stream), duration, widthAndHeight);
            }

            Assert.Equal(MessageType.VideoNoteMessage, message.Type);
            Assert.Equal(duration, message.VideoNote.Duration);
            Assert.Equal(widthAndHeight, message.VideoNote.Width);
            Assert.Equal(widthAndHeight, message.VideoNote.Height);
            Assert.NotNull(message.VideoNote.Thumb);

            _fixture.GiveMessageToNextTest = message;
        }

        [Fact(DisplayName = FactTitles.ShouldSerializeAndDeserializeVideoNoteMessage)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.JsonConvert)]
        [ExecutionOrder(2.4)]
        public async Task ShouldSerializeAndDeserializeVideoNoteMessage()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSerializeAndDeserializeVideoNoteMessage);

            Message message = _fixture.GiveMessageToNextTest;
            string json = JsonConvert.SerializeObject(message);
            Message message2 = JsonConvert.DeserializeObject<Message>(json);

            Assert.Equal(MessageType.VideoNoteMessage, message2.Type);
            Assert.Equal(message.VideoNote.Duration, message2.VideoNote.Duration);
            Assert.Equal(message.VideoNote.Width, message2.VideoNote.Width);
            Assert.Equal(message.VideoNote.Height, message2.VideoNote.Height);
            Assert.Equal(message.VideoNote.Thumb, message2.VideoNote.Thumb);
        }

        #endregion

        #region 3. Sending documents

        [Fact(DisplayName = FactTitles.ShouldSendPdf)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.TelegramBotApiMethods.SendDocument)]
        [ExecutionOrder(3.1)]
        public async Task Should_Send_Pdf_Document()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendPdf);

            const string caption = "The Tragedy of Hamlet,\nPrince of Denmark";
            const string mimeType = "application/pdf";
            const string fileName = "hamlet.pdf";
            const int fileSize = 256984;

            Message message;
            using (var stream = new FileStream("Files/Document/hamlet.pdf", FileMode.Open))
            {
                message = await BotClient.SendDocumentAsync(_fixture.SuperGroupChatId,
                    new FileToSend(fileName, stream), caption);
            }

            Assert.Equal(MessageType.DocumentMessage, message.Type);
            Assert.Equal(fileName, message.Document.FileName);
            Assert.Equal(mimeType, message.Document.MimeType);
            Assert.InRange(Math.Abs(fileSize - message.Document.FileSize), 0, 3500);
            Assert.InRange(message.Document.FileId.Length, 20, 40);
            Assert.Equal(caption, message.Caption);

            _fixture.GiveMessageToNextTest = message;
        }

        [Fact(DisplayName = FactTitles.ShouldSerializeAndDeserializeDocumentMessage)]
        [Trait(CommonConstants.MethodTraitName, CommonConstants.JsonConvert)]
        [ExecutionOrder(3.2)]
        public async Task ShouldSerializeAndDeserializeDocumentMessage()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSerializeAndDeserializeDocumentMessage);

            Message message = _fixture.GiveMessageToNextTest;
            string json = JsonConvert.SerializeObject(message);
            Message message2 = JsonConvert.DeserializeObject<Message>(json);

            Assert.Equal(MessageType.DocumentMessage, message2.Type);
            Assert.Equal(message.Document.FileName, message2.Document.FileName);
            Assert.Equal(message.Document.MimeType, message2.Document.MimeType);
            Assert.InRange(Math.Abs(message.Document.FileSize - message2.Document.FileSize), 0, 3500);
            Assert.InRange(message2.Document.FileId.Length, 20, 40);
            Assert.Equal(message.Caption, message2.Caption);
        }

        #endregion

        private static class FactTitles
        {
            public const string ShouldSendPhotoUsingFileId = "Should Send the same photo twice using file_id";

            public const string ShouldSerializeAndDeserializePhotoMessage = "Should serialize and deserialize photo message";

            public const string ShouldSendVideo = "Should send a video with caption";

            public const string ShouldSerializeAndDeserializeVideoMessage = "Should serialize and deserialize video message";

            public const string ShouldSendVideoNote = "Should send a video note";

            public const string ShouldSerializeAndDeserializeVideoNoteMessage = "Should serialize and deserialize video note message";

            public const string ShouldSendPdf = "Should send a pdf document with caption";

            public const string ShouldSerializeAndDeserializeDocumentMessage = "Should serialize and deserialize document message";
        }
    }
}
