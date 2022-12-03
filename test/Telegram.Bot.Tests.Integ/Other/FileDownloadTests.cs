using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;
using Xunit.Abstractions;
using File = Telegram.Bot.Types.File;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.FileDownload)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class FileDownloadTests : IClassFixture<FileDownloadTests.Fixture>
{
    readonly ITestOutputHelper _output;
    readonly Fixture _classFixture;
    readonly TestsFixture _fixture;

    ITelegramBotClient BotClient => _fixture.BotClient;

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
        await using (Stream stream = System.IO.File.OpenRead(Constants.PathToFile.Documents.Hamlet))
        {
            documentMessage = await BotClient.SendDocumentAsync(
                chatId: _fixture.SupergroupChat,
                document: new InputFile(stream)
            );
        }

        string fileId = documentMessage.Document!.FileId;

        #endregion

        File file = await BotClient.GetFileAsync(documentMessage.Document.FileId);

        Assert.Equal(fileId, file.FileId);
        Assert.NotNull(file.FileSize);
        Assert.InRange((int)file.FileSize, fileSize - 3500, fileSize + 3500);
        Assert.NotNull(file.FilePath);
        Assert.NotEmpty(file.FilePath);

        _classFixture.File = file;
    }

    [OrderedFact("Should download file using file_path and write it to disk")]
    public async Task Should_Download_Write_Using_FilePath()
    {
        long? fileSize = _classFixture.File.FileSize;

        string destinationFilePath = $"{Path.GetTempFileName()}.{Fixture.FileType}";
        _output.WriteLine($@"Writing file to ""{destinationFilePath}""");

        await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
        await BotClient.DownloadFileAsync(
            filePath: _classFixture.File.FilePath!,
            destination: fileStream
        );

        Assert.NotNull(fileSize);
        Assert.InRange(fileStream.Length, (int)fileSize - 100, (int)fileSize + 100);
    }

    [OrderedFact("Should download file using file_id and write it to disk")]
    public async Task Should_Download_Write_Using_FileId()
    {
        long? fileSize = _classFixture.File.FileSize;

        string destinationFilePath = $"{Path.GetTempFileName()}.{Fixture.FileType}";
        _output.WriteLine($@"Writing file to ""{destinationFilePath}""");

        await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
        File file = await BotClient.GetInfoAndDownloadFileAsync(
            fileId: _classFixture.File.FileId,
            destination: fileStream
        );

        Assert.NotNull(fileSize);
        Assert.InRange(fileStream.Length, (int)fileSize - 100, (int)fileSize + 100);
        Assert.True(JToken.DeepEquals(
            JToken.FromObject(_classFixture.File), JToken.FromObject(file)
        ));
    }

    [OrderedFact("Should throw InvalidParameterException while trying to get file using wrong file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetFile)]
    public async Task Should_Throw_FileId_InvalidParameterException()
    {
        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(async () =>
            await BotClient.GetFileAsync("Invalid_File_id")
        );

        Assert.Contains("file_id", exception.Message);
    }

    [OrderedFact("Should throw HttpRequestException while trying to download file using wrong file_path")]
    public async Task Should_Throw_FilePath_HttpRequestException()
    {
        Stream content = default;

        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            await BotClient.DownloadFileAsync("Invalid_File_Path", content);
        });

        Assert.Null(content);
    }

    public class Fixture
    {
        public const string FileType = "pdf";

        public File File { get; set; }
    }
}
