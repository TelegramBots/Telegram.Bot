using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Sending_Messages
{
    [Collection(Constants.TestCollections.SendDocumentMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class SendingDocumentMessageTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public SendingDocumentMessageTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendPdf)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
        public async Task Should_Send_Pdf_Document()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendPdf);

            const string caption = "The Tragedy of Hamlet,\nPrince of Denmark";
            const string mimeType = "application/pdf";
            const string fileName = "hamlet.pdf";
            const int fileSize = 256984;

            Message message;
            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Documents.Hamlet))
            {
                message = await BotClient.SendDocumentAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    document: new InputOnlineFile(stream, fileName),
                    caption: caption
                );
            }

            Assert.Equal(MessageType.Document, message.Type);
            Assert.Equal(fileName, message.Document.FileName);
            Assert.Equal(mimeType, message.Document.MimeType);
            Assert.InRange(Math.Abs(fileSize - message.Document.FileSize), 0, 3500);
            Assert.InRange(message.Document.FileId.Length, 20, 40);
            Assert.Equal(caption, message.Caption);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendDocumentWithNonAsciiName)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
        public async Task Should_Send_Document_With_Farsi_Name()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendDocumentWithNonAsciiName);

            const string caption = "تراژدی هملت\nشاهزاده دانمارک";
            const string mimeType = "application/pdf";
            const string fileName = "هملت.pdf";
            const int fileSize = 256984;

            Message message;

            using (Stream stream = System.IO.File.OpenRead(Constants.FileNames.Documents.Hamlet))
            {
                message = await BotClient.SendDocumentAsync(
                    chatId: _fixture.SupergroupChat.Id,
                    document: new InputOnlineFile(stream, fileName),
                    caption: caption
                );
            }

            Assert.Equal(MessageType.Document, message.Type);
            Assert.Equal(fileName, message.Document.FileName);
            Assert.Equal(mimeType, message.Document.MimeType);
            Assert.InRange(Math.Abs(fileSize - message.Document.FileSize), 0, 3500);
            Assert.InRange(message.Document.FileId.Length, 20, 40);
            Assert.Equal(caption, message.Caption);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldSendDocumentWithThumb)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
        public async Task Should_Send_Document_With_Thumb()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldSendDocumentWithThumb);

            Message message;
            using (Stream
                stream1 = System.IO.File.OpenRead(Constants.FileNames.Documents.Hamlet),
                stream2 = System.IO.File.OpenRead(Constants.FileNames.Thumbnail.TheAbilityToBreak)
            )
            {
                message = await BotClient.SendDocumentAsync(
                    /* chatId: */ _fixture.SupergroupChat,
                    /* document: */ new InputMedia(stream1, "Hamlet.pdf"),
                    thumb: new InputMedia(stream2, "thumb.jpg")
                );
            }

            Assert.NotNull(message.Document.Thumb);
            Assert.NotEmpty(message.Document.Thumb.FileId);
            Assert.Equal(90, message.Document.Thumb.Height);
            Assert.Equal(90, message.Document.Thumb.Width);
            Assert.True(message.Document.Thumb.FileSize > 10_000);
        }

        private static class FactTitles
        {
            public const string ShouldSendPdf = "Should send a pdf document with caption";

            public const string ShouldSendDocumentWithNonAsciiName =
                "Should send a pdf document having a Farsi(non-ASCII) file name";

            public const string ShouldSendDocumentWithThumb = "Should send a pdf document with thumbnail";
        }
    }
}
