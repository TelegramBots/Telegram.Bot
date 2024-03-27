using System.IO;
using Telegram.Bot.Types;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.Serialization;

public class InputFileSerializationTests
{
    [Fact(DisplayName = "Should serialize & deserialize input file from stream")]
    public void Should_Serialize_InputFile()
    {
        const string fileName = "myFile";
        using MemoryStream memoryStream = new();
        InputFileStream inputFile = new(memoryStream, fileName);

        string serializedValue = JsonSerializer.Serialize(inputFile, JsonSerializerOptionsProvider.Options);

        Assert.Equal(@$"""attach://{fileName}""", serializedValue);
    }

    [Fact(DisplayName = "Should serialize & deserialize input file with file_id")]
    public void Should_Serialize_FileId()
    {
        const string fileId = "This-is-a-file_id";
        InputFileId inputFileId = new(fileId);

        string serializedValue = JsonSerializer.Serialize(inputFileId, JsonSerializerOptionsProvider.Options);

        Assert.Equal($"\"{fileId}\"", serializedValue);
    }

    [Fact(DisplayName = "Should serialize & deserialize input file with URL")]
    public void Should_Serialize_InputUrlFile()
    {
        Uri url = new("https://github.com/TelegramBots");
        InputFileUrl inputFileUrl = new(url);

        string serializedValue = JsonSerializer.Serialize(inputFileUrl, JsonSerializerOptionsProvider.Options);

        Assert.Equal($"\"{url}\"", serializedValue);
    }
}
