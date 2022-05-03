using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class Test_InlineQueryResultTypeConverter
{
    [Theory]
    [InlineData(InlineQueryResultType.Unknown, "unknown")]
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
    public void Sould_Convert_InlineQueryResultType_To_String(InlineQueryResultType inlineQueryResultType, string value)
    {
        InlineQueryResult inlineQuery = new InlineQueryResult() { Type = inlineQueryResultType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(inlineQuery);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(InlineQueryResultType.Unknown, "unknown")]
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
    public void Sould_Convert_String_To_InlineQueryResultType(InlineQueryResultType inlineQueryResultType, string value)
    {
        InlineQueryResult expectedResult = new InlineQueryResult() { Type = inlineQueryResultType };
        string jsonData = @$"{{""type"":""{value}""}}";

        InlineQueryResult result = JsonConvert.DeserializeObject<InlineQueryResult>(jsonData);

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Sould_Return_Zero_For_Incorrect_InlineQueryResultType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        InlineQueryResult result = JsonConvert.DeserializeObject<InlineQueryResult>(jsonData);

        Assert.Equal((InlineQueryResultType)0, result.Type);
    }

    [Fact]
    public void Sould_Convert_To_Unknown_For_Incorrect_InlineQueryResultType()
    {
        InlineQueryResult inlineQueryResult = new InlineQueryResult() { Type = (InlineQueryResultType)int.MaxValue };
        const string expectedResult = @"{""type"":""unknown""}";

        string result = JsonConvert.SerializeObject(inlineQueryResult);

        Assert.Equal(expectedResult, result);
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    private class InlineQueryResult
    {
        [JsonProperty(Required = Required.Always)]
        public InlineQueryResultType Type { get; init; }
    }
}
