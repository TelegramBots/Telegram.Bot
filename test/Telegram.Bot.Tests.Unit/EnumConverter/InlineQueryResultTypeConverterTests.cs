using Telegram.Bot.Types.InlineQueryResults;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class InlineQueryResultTypeConverterTests
{
    [Theory]
    [InlineData((InlineQueryResultType)0, "unknown")]
    [InlineData(InlineQueryResultType.Article, "article")]
    [InlineData(InlineQueryResultType.Photo, "photo")]
    [InlineData(InlineQueryResultType.Gif, "gif")]
    [InlineData(InlineQueryResultType.Mpeg4Gif, "mpeg4_gif")]
    [InlineData(InlineQueryResultType.Video, "video")]
    [InlineData(InlineQueryResultType.Audio, "audio")]
    [InlineData(InlineQueryResultType.Contact, "contact")]
    [InlineData(InlineQueryResultType.Document, "document")]
    [InlineData(InlineQueryResultType.Location, "location")]
    [InlineData(InlineQueryResultType.Venue, "venue")]
    [InlineData(InlineQueryResultType.Voice, "voice")]
    [InlineData(InlineQueryResultType.Game, "game")]
    [InlineData(InlineQueryResultType.Sticker, "sticker")]
    public void Should_Convert_InlineQueryResultType_To_String(InlineQueryResultType inlineQueryResultType, string value)
    {
        InlineQueryResult inlineQuery = new() { Type = inlineQueryResultType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonSerializer.Serialize(inlineQuery, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData((InlineQueryResultType)0, "unknown")]
    [InlineData(InlineQueryResultType.Article, "article")]
    [InlineData(InlineQueryResultType.Photo, "photo")]
    [InlineData(InlineQueryResultType.Gif, "gif")]
    [InlineData(InlineQueryResultType.Mpeg4Gif, "mpeg4_gif")]
    [InlineData(InlineQueryResultType.Video, "video")]
    [InlineData(InlineQueryResultType.Audio, "audio")]
    [InlineData(InlineQueryResultType.Contact, "contact")]
    [InlineData(InlineQueryResultType.Document, "document")]
    [InlineData(InlineQueryResultType.Location, "location")]
    [InlineData(InlineQueryResultType.Venue, "venue")]
    [InlineData(InlineQueryResultType.Voice, "voice")]
    [InlineData(InlineQueryResultType.Game, "game")]
    [InlineData(InlineQueryResultType.Sticker, "sticker")]
    public void Should_Convert_String_To_InlineQueryResultType(InlineQueryResultType inlineQueryResultType, string value)
    {
        InlineQueryResult expectedResult = new() { Type = inlineQueryResultType };
        string jsonData = @$"{{""type"":""{value}""}}";

        InlineQueryResult? result = JsonSerializer.Deserialize<InlineQueryResult>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_InlineQueryResultType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        InlineQueryResult? result = JsonSerializer.Deserialize<InlineQueryResult>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((InlineQueryResultType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_InlineQueryResultType()
    {
        InlineQueryResult inlineQueryResult = new() { Type = (InlineQueryResultType)int.MaxValue };

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(inlineQueryResult, JsonSerializerOptionsProvider.Options));
    }


    class InlineQueryResult
    {
        [JsonRequired]
        public InlineQueryResultType Type { get; init; }
    }
}
