using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class FileTypeConverterTests
{
    [Theory(Skip = "Doesn't make much sense since FileType never gets serialized in the API")]
    [InlineData(FileType.Stream, "stream")]
    [InlineData(FileType.Id, "id")]
    [InlineData(FileType.Url, "url")]
    public void Should_Convert_FileType_To_String(FileType fileType, string value)
    {
        OnlineFile onlineFile = new() { FileType = fileType };
        string expectedResult = @$"{{""file_type"":""{value}""}}";

        string result = JsonSerializer.Serialize(onlineFile);

        Assert.Equal(expectedResult, result);
    }

    [Theory(Skip = "Doesn't make much sense since FileType never gets serialized in the API")]
    [InlineData(FileType.Stream, "stream")]
    [InlineData(FileType.Id, "id")]
    [InlineData(FileType.Url, "url")]
    public void Should_Convert_String_To_FileType(FileType fileType, string value)
    {
        OnlineFile expectedResult = new() { FileType = fileType };
        string jsonData = @$"{{""file_type"":""{value}""}}";

        OnlineFile? result = JsonSerializer.Deserialize<OnlineFile>(jsonData);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.FileType, result.FileType);
    }

    [Fact(Skip = "Doesn't make much sense since FileType never gets serialized in the API")]
    public void Should_Return_Zero_For_Incorrect_FileType()
    {
        string jsonData = @$"{{""file_type"":""{int.MaxValue}""}}";

        OnlineFile? result = JsonSerializer.Deserialize<OnlineFile>(jsonData);

        Assert.NotNull(result);
        Assert.Equal((FileType)0, result.FileType);
    }

    [Fact(Skip = "Doesn't make much sense since FileType never gets serialized in the API")]
    public void Should_Throw_JsonException_For_Incorrect_FileType()
    {
        OnlineFile onlineFile = new() { FileType = (FileType)int.MaxValue };

        // ToDo: add FileType.Unknown ?
        //    protected override string GetStringValue(FileType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(onlineFile));
    }


    class OnlineFile
    {
        [JsonRequired]
        public FileType FileType { get; init; }
    }
}
