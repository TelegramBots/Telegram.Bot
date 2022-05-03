using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class Test_ChatTypeConverter
{
    [Theory]
    [InlineData(ChatType.Private, "private")]
    [InlineData(ChatType.Group, "group")]
    [InlineData(ChatType.Channel, "channel")]
    [InlineData(ChatType.Supergroup, "supergroup")]
    [InlineData(ChatType.Sender, "sender")]
    public void Sould_Convert_ChatType_To_String(ChatType chatType, string value)
    {
        InlineQuery inlineQuery = new InlineQuery() { Type = chatType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(inlineQuery);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(ChatType.Private, "private")]
    [InlineData(ChatType.Group, "group")]
    [InlineData(ChatType.Channel, "channel")]
    [InlineData(ChatType.Supergroup, "supergroup")]
    [InlineData(ChatType.Sender, "sender")]
    public void Sould_Convert_String_To_ChatType(ChatType chatType, string value)
    {
        InlineQuery expectedResult = new InlineQuery() { Type = chatType };
        string jsonData = @$"{{""type"":""{value}""}}";

        InlineQuery result = JsonConvert.DeserializeObject<InlineQuery>(jsonData);

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Sould_Return_Zero_For_Incorrect_ChatType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        InlineQuery result = JsonConvert.DeserializeObject<InlineQuery>(jsonData);

        Assert.Equal((ChatType)0, result.Type);
    }

    [Fact]
    public void Sould_Throw_NotSupportedException_For_Incorrect_ChatType()
    {
        InlineQuery inlineQuery = new InlineQuery() { Type = (ChatType)int.MaxValue };

        // ToDo: add ChatType.Unknown ?
        //    protected override string GetStringValue(ChatType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        NotSupportedException ex = Assert.Throws<NotSupportedException>(() =>
            JsonConvert.SerializeObject(inlineQuery));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    private class InlineQuery
    {
        [JsonProperty(Required = Required.Always)]
        public ChatType Type { get; init; }
    }
}
