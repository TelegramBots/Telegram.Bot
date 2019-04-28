using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;
using Xunit.Abstractions;
using File = Telegram.Bot.Types.File;

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

        [OrderedFact("Should get file info")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetFile)]
        public async Task Should_Get_File_Info()
        {
            const int fileSize = 253736;

            #region Send Document

            Message documentMessage;
            using (FileStream stream = System.IO.File.OpenRead(Constants.PathToFile.Documents.Hamlet))
            {
                documentMessage = await BotClient.SendDocumentAsync(
                    /* chatId: */ _fixture.SupergroupChat,
                    /* document: */ stream
                );
            }

            #endregion

            File file = await BotClient.GetFileAsync(documentMessage.Document.FileId);

            Assert.Equal(documentMessage.Document.FileId, file.FileId);
            Assert.InRange(file.FileSize, fileSize - 3500, fileSize + 3500);
            Assert.NotEmpty(file.FilePath);

            _classFixture.File = file;
        }

        [OrderedFact("Should download file using file_path")]
        public async Task Should_Download_Using_FilePath()
        {
            int fileSize = _classFixture.File.FileSize;

            using (MemoryStream destinationStream = new MemoryStream())
            {
                await BotClient.DownloadFileAsync(
                    /* filePath: */ _classFixture.File.FilePath,
                    /* destination: */ destinationStream
                );

                Assert.InRange(destinationStream.Length, fileSize - 100, fileSize + 100);
            }
        }

        [OrderedFact("Should download file using file_path and write it to disk")]
        public async Task Should_Download_Write_Using_FilePath()
        {
            int fileSize = _classFixture.File.FileSize;

            string destinationFilePath = $"{System.IO.Path.GetTempFileName()}.{Fixture.FileType}";
            _output.WriteLine($@"Writing file to ""{destinationFilePath}""");

            using (FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath))
            {
                await BotClient.DownloadFileAsync(
                    /* filePath: */ _classFixture.File.FilePath,
                    /* destination: */ fileStream
                );

                Assert.InRange(fileStream.Length, fileSize - 100, fileSize + 100);
            }
        }

        [OrderedFact("Should download file using file_id and write it to disk")]
        public async Task Should_Download_Write_Using_FileId()
        {
            int fileSize = _classFixture.File.FileSize;

            string destinationFilePath = $"{System.IO.Path.GetTempFileName()}.{Fixture.FileType}";
            _output.WriteLine($@"Writing file to ""{destinationFilePath}""");

            using (FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath))
            {
                File file = await BotClient.GetInfoAndDownloadFileAsync(
                    /* fileId: */ _classFixture.File.FileId,
                    /* destination: */ fileStream
                );

                Assert.InRange(fileStream.Length, fileSize - 100, fileSize + 100);
                Assert.True(JToken.DeepEquals(
                    JToken.FromObject(_classFixture.File), JToken.FromObject(file)
                ));
            }
        }

        [OrderedFact("Should throw InvalidParameterException while trying to get file using wrong file_id")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetFile)]
        public async Task Should_Throw_FileId_InvalidParameterException()
        {
            InvalidParameterException exception = await Assert.ThrowsAnyAsync<InvalidParameterException>(
                () => BotClient.GetFileAsync("Invalid_File_id")
            );

            Assert.Equal("file id", exception.Parameter);
        }

        [OrderedFact("Should throw HttpRequestException while trying to download file using wrong file_path")]
        public async Task Should_Throw_FilePath_HttpRequestException()
        {
            using (MemoryStream destinationStream = new MemoryStream())
            {
                HttpRequestException exception = await Assert.ThrowsAnyAsync<HttpRequestException>(async () =>
                {
                    await BotClient.DownloadFileAsync(
                        /* filePath: */ "Invalid_File_Path",
                        /* destination: */ destinationStream
                    );
                });

                Assert.Contains("404", exception.Message);
                Assert.Equal(0, destinationStream.Length);
            }
        }

        public class Fixture
        {
            public const string FileType = "pdf";

            public File File { get; set; }
        }
    }
}
