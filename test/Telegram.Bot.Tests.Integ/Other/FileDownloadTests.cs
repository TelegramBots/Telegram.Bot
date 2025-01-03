using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Xunit;
using Xunit.Abstractions;

namespace Telegram.Bot.Tests.Integ.Other;

[Collection(Constants.TestCollections.FileDownload)]
[TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
public class FileDownloadTests(TestsFixture fixture, FileDownloadTests.ClassFixture classFixture, ITestOutputHelper output)
    : TestClass(fixture), IClassFixture<FileDownloadTests.ClassFixture>
{
    [OrderedFact("Should get file info")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetFile)]
    public async Task Should_Get_File_Info()
    {
        long fileSize = 253736;

        #region Send Document

        Message documentMessage;
        await using (FileStream stream = File.OpenRead(Constants.PathToFile.Documents.Hamlet))
        {
            fileSize = stream.Length;
            documentMessage = await BotClient.WithStreams(stream).SendDocument(
                chatId: Fixture.SupergroupChat,
                document: InputFile.FromStream(stream)
            );
        }

        string fileId = documentMessage.Document!.FileId;

        #endregion

        TGFile file = await BotClient.GetFile(documentMessage.Document.FileId);

        Assert.Equal(fileId, file.FileId);
        Assert.NotNull(file.FileSize);
        Assert.InRange((int)file.FileSize, fileSize - 3500, fileSize + 3500);
        Assert.NotNull(file.FilePath);
        Assert.NotEmpty(file.FilePath);

        classFixture.File = file;
    }

    [OrderedFact("Should download file using file_path and write it to disk")]
    public async Task Should_Download_Write_Using_FilePath()
    {
        long? fileSize = classFixture.File.FileSize;

        string destinationFilePath = $"{Path.GetTempFileName()}.{ClassFixture.FileType}";
        output.WriteLine($@"Writing file to ""{destinationFilePath}""");

        await using FileStream fileStream = File.OpenWrite(destinationFilePath);
        await BotClient.DownloadFile(
            filePath: classFixture.File.FilePath!,
            destination: fileStream
        );

        Assert.NotNull(fileSize);
        Assert.InRange(fileStream.Length, (int)fileSize - 100, (int)fileSize + 100);
    }

    [OrderedFact("Should download file using file_id and write it to disk")]
    public async Task Should_Download_Write_Using_FileId()
    {
        long? fileSize = classFixture.File.FileSize;

        string destinationFilePath = $"{Path.GetTempFileName()}.{ClassFixture.FileType}";
        output.WriteLine($@"Writing file to ""{destinationFilePath}""");

        await using FileStream fileStream = File.OpenWrite(destinationFilePath);
        TGFile file = await BotClient.GetInfoAndDownloadFile(
            fileId: classFixture.File.FileId,
            destination: fileStream
        );

        Assert.NotNull(fileSize);
        Assert.InRange(fileStream.Length, (int)fileSize - 100, (int)fileSize + 100);
        Asserts.JsonEquals(classFixture.File, file);
    }

    [OrderedFact("Should throw InvalidParameterException while trying to get file using wrong file_id")]
    [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.GetFile)]
    public async Task Should_Throw_FileId_InvalidParameterException()
    {
        ApiRequestException exception = await Assert.ThrowsAsync<ApiRequestException>(async () =>
            await BotClient.GetFile("Invalid_File_id")
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
            await BotClient.DownloadFile("Invalid_File_Path", content);
        });

        Assert.Null(content);
    }

    public class ClassFixture
    {
        public const string FileType = "pdf";

        public TGFile File { get; set; }
    }
}
