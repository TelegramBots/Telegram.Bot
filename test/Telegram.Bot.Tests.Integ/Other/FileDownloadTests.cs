using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;
using Xunit.Abstractions;

namespace Telegram.Bot.Tests.Integ.Other
{
    [Collection(Constants.TestCollections.FileDownload)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class FileDownloadTests : IClassFixture<FileDownloadTests.Fixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly ITestOutputHelper _output;

        private readonly Fixture _classFixture;

        private readonly TestsFixture _fixture;

        public FileDownloadTests(TestsFixture fixture, Fixture classFixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _classFixture = classFixture;
            _output = output;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldGetFileInfo)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetFile)]
        public async Task Should_Get_File_Info()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldGetFileInfo);

            const int fileSize = 253736;
            string fileId;

            #region Send Document

            Message documentMessage;
            using (System.IO.Stream stream = System.IO.File.OpenRead(Constants.FileNames.Documents.Hamlet))
            {
                documentMessage = await BotClient.SendDocumentAsync(
                    chatId: _fixture.SupergroupChat,
                    document: stream
                );
            }

            fileId = documentMessage.Document.FileId;

            #endregion

            File file = await BotClient.GetFileAsync(documentMessage.Document.FileId);

            Assert.Equal(fileId, file.FileId);
            Assert.InRange(file.FileSize, fileSize - 3500, fileSize + 3500);
            Assert.NotEmpty(file.FilePath);

            _classFixture.File = file;
        }

        [OrderedFact(DisplayName = FactTitles.ShouldDownloadUsingFilePath)]
        public async Task Should_Download_Using_FilePath()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDownloadUsingFilePath);

            int fileSize = _classFixture.File.FileSize;

            System.IO.Stream stream = await BotClient.DownloadFileAsync(
                filePath: _classFixture.File.FilePath
            );

            Assert.InRange(stream.Length, fileSize - 100, fileSize + 100);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldDownloadWriteUsingFilePath)]
        public async Task Should_Download_Write_Using_FilePath()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDownloadWriteUsingFilePath);

            int fileSize = _classFixture.File.FileSize;

            string destinationFilePath = $"{System.IO.Path.GetTempFileName()}.{Fixture.FileType}";
            _output.WriteLine($@"Writing file to ""{destinationFilePath}""");

            System.IO.FileStream fileStream;
            using (fileStream = System.IO.File.OpenWrite(destinationFilePath))
            {
                await BotClient.DownloadFileAsync(
                    filePath: _classFixture.File.FilePath,
                    destination: fileStream
                );

                Assert.InRange(fileStream.Length, fileSize - 100, fileSize + 100);
            }
        }

        [OrderedFact(DisplayName = FactTitles.ShouldDownloadWriteUsingFileId)]
        public async Task Should_Download_Write_Using_FileId()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldDownloadWriteUsingFileId);

            int fileSize = _classFixture.File.FileSize;

            string destinationFilePath = $"{System.IO.Path.GetTempFileName()}.{Fixture.FileType}";
            _output.WriteLine($@"Writing file to ""{destinationFilePath}""");

            File file;
            System.IO.FileStream fileStream;
            using (fileStream = System.IO.File.OpenWrite(destinationFilePath))
            {
                file = await BotClient.GetInfoAndDownloadFileAsync(
                    fileId: _classFixture.File.FileId,
                    destination: fileStream
                );

                Assert.InRange(fileStream.Length, fileSize - 100, fileSize + 100);
                Assert.True(JToken.DeepEquals(
                    JToken.FromObject(_classFixture.File), JToken.FromObject(file)
                ));
            }
        }
        
        [OrderedFact(DisplayName = FactTitles.ShouldThrowInvalidParameterExceptionForFileId)]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetFile)]
        public async Task Should_Throw_FileId_InvalidParameterException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidParameterExceptionForFileId);

            InvalidParameterException exception = await Assert.ThrowsAnyAsync<InvalidParameterException>(
                () => BotClient.GetFileAsync("Invalid_File_id")
            );

            Assert.Equal("file id", exception.Parameter);
        }

        [OrderedFact(DisplayName = FactTitles.ShouldThrowInvalidHttpRequestExceptionForFilePath)]
        public async Task Should_Throw_FilePath_HttpRequestException()
        {
            await _fixture.SendTestCaseNotificationAsync(FactTitles.ShouldThrowInvalidHttpRequestExceptionForFilePath);

            System.IO.Stream content = default;

            HttpRequestException exception = await Assert.ThrowsAnyAsync<HttpRequestException>(async () =>
            {
                content = await BotClient.DownloadFileAsync("Invalid_File_Path");
            });

            Assert.Contains("404", exception.Message);
            Assert.Null(content);
        }

        private static class FactTitles
        {
            public const string ShouldGetFileInfo = "Should get file info";

            public const string ShouldDownloadUsingFilePath = "Should download file using file_path";

            public const string ShouldDownloadWriteUsingFilePath =
                "Should download file using file_path and write it to disk";

            public const string ShouldDownloadWriteUsingFileId =
                "Should download file using file_id and write it to disk";

            public const string ShouldThrowInvalidParameterExceptionForFileId =
                "Should throw InvalidParameterException while trying to get file using wrong file_id";

            public const string ShouldThrowInvalidHttpRequestExceptionForFilePath =
                "Should throw HttpRequestException while trying to download file using wrong file_path";
        }

        public class Fixture
        {
            public const string FileType = "pdf";

            public File File { get; set; }
        }
    }
}