using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class FileTypeConverterTests
{
    [Theory]
    [InlineData(FileType.Stream, "stream")]
    [InlineData(FileType.Id, "id")]
    [InlineData(FileType.Url, "url")]
    public void Should_Convert_FileType_To_String(FileType fileType, string value)
    {
        OnlineFile onlineFile = new() { FileType = fileType };
        string expectedResult = @$"{{""file_type"":""{value}""}}";

        string result = JsonSerializer.Serialize(onlineFile, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(FileType.Stream, "stream")]
    [InlineData(FileType.Id, "id")]
    [InlineData(FileType.Url, "url")]
    public void Should_Convert_String_To_FileType(FileType fileType, string value)
    {
        OnlineFile expectedResult = new() { FileType = fileType };
        string jsonData = @$"{{""file_type"":""{value}""}}";

        OnlineFile? result = JsonSerializer.Deserialize<OnlineFile>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.FileType, result.FileType);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_FileType()
    {
        string jsonData = @$"{{""file_type"":""{int.MaxValue}""}}";

        OnlineFile? result = JsonSerializer.Deserialize<OnlineFile>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((FileType)0, result.FileType);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_FileType()
    {
        OnlineFile onlineFile = new() { FileType = (FileType)int.MaxValue };

        // ToDo: add FileType.Unknown ?
        //    protected override string GetStringValue(FileType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(onlineFile, JsonSerializerOptionsProvider.Options));
    }


    class OnlineFile
    {
        [JsonRequired]
        public FileType FileType { get; init; }
    }
}
