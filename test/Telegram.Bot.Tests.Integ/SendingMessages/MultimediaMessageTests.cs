using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Common;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Integ.SendingMessages
{
    [Collection(Constants.TestCollections.MultimediaMessage)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class MultimediaMessageTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public MultimediaMessageTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        #region 2. Sending videos


        #endregion

        #region 3. Sending documents

        [Fact(DisplayName = FactTitles.ShouldSendPdf)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
        [ExecutionOrder(3.1)]
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
                message = await BotClient.SendDocumentAsync(_fixture.SuperGroupChatId,
                    new FileToSend(fileName, stream), caption);
            }

            Assert.Equal(MessageType.DocumentMessage, message.Type);
            Assert.Equal(fileName, message.Document.FileName);
            Assert.Equal(mimeType, message.Document.MimeType);
            Assert.InRange(Math.Abs(fileSize - message.Document.FileSize), 0, 3500);
            Assert.InRange(message.Document.FileId.Length, 20, 40);
            Assert.Equal(caption, message.Caption);
        }

        [Fact(DisplayName = FactTitles.ShouldSendDocumentWithNonAsciiName)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendDocument)]
        [ExecutionOrder(3.2)]
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
                message = await BotClient.SendDocumentAsync(_fixture.SuperGroupChatId,
                    new FileToSend(fileName, stream), caption);
            }

            Assert.Equal(MessageType.DocumentMessage, message.Type);
            Assert.Equal(fileName, message.Document.FileName);
            Assert.Equal(mimeType, message.Document.MimeType);
            Assert.InRange(Math.Abs(fileSize - message.Document.FileSize), 0, 3500);
            Assert.InRange(message.Document.FileId.Length, 20, 40);
            Assert.Equal(caption, message.Caption);
        }

        #endregion

        private static class FactTitles
        {
            public const string ShouldSendPdf = "Should send a pdf document with caption";

            public const string ShouldSendDocumentWithNonAsciiName = "Should send a pdf document having a Farsi(non-ASCII) file name";
        }
    }
}
