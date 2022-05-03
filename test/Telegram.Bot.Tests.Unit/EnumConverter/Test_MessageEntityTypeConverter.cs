using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class Test_MessageEntityTypeConverter
{
    [Theory]
    [InlineData(MessageEntityType.Mention, "mention" )]
    [InlineData(MessageEntityType.Hashtag, "hashtag" )]
    [InlineData(MessageEntityType.BotCommand, "bot_command" )]
    [InlineData(MessageEntityType.Url, "url" )]
    [InlineData(MessageEntityType.Email, "email" )]
    [InlineData(MessageEntityType.Bold, "bold" )]
    [InlineData(MessageEntityType.Italic, "italic" )]
    [InlineData(MessageEntityType.Code, "code" )]
    [InlineData(MessageEntityType.Pre, "pre" )]
    [InlineData(MessageEntityType.TextLink, "text_link" )]
    [InlineData(MessageEntityType.TextMention, "text_mention" )]
    [InlineData(MessageEntityType.PhoneNumber, "phone_number" )]
    [InlineData(MessageEntityType.Cashtag, "cashtag" )]
    [InlineData(MessageEntityType.Underline, "underline" )]
    [InlineData(MessageEntityType.Strikethrough, "strikethrough" )]
    [InlineData(MessageEntityType.Spoiler, "spoiler" )]
    public void Sould_Convert_MessageEntityType_To_String(MessageEntityType messageEntityType, string value)
    {
        MessageEntity messageEntity = new MessageEntity() { Type = messageEntityType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(messageEntity);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(MessageEntityType.Mention, "mention")]
    [InlineData(MessageEntityType.Hashtag, "hashtag")]
    [InlineData(MessageEntityType.BotCommand, "bot_command")]
    [InlineData(MessageEntityType.Url, "url")]
    [InlineData(MessageEntityType.Email, "email")]
    [InlineData(MessageEntityType.Bold, "bold")]
    [InlineData(MessageEntityType.Italic, "italic")]
    [InlineData(MessageEntityType.Code, "code")]
    [InlineData(MessageEntityType.Pre, "pre")]
    [InlineData(MessageEntityType.TextLink, "text_link")]
    [InlineData(MessageEntityType.TextMention, "text_mention")]
    [InlineData(MessageEntityType.PhoneNumber, "phone_number")]
    [InlineData(MessageEntityType.Cashtag, "cashtag")]
    [InlineData(MessageEntityType.Underline, "underline")]
    [InlineData(MessageEntityType.Strikethrough, "strikethrough")]
    [InlineData(MessageEntityType.Spoiler, "spoiler")]
    public void Sould_Convert_String_To_MessageEntityType(MessageEntityType messageEntityType, string value)
    {
        MessageEntity expectedResult = new MessageEntity() { Type = messageEntityType };
        string jsonData = @$"{{""type"":""{value}""}}";

        MessageEntity result = JsonConvert.DeserializeObject<MessageEntity>(jsonData);

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Sould_Return_Zero_For_Incorrect_MessageEntityType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        MessageEntity result = JsonConvert.DeserializeObject<MessageEntity>(jsonData);

        Assert.Equal((MessageEntityType)0, result.Type);
    }

    [Fact]
    public void Sould_Throw_NotSupportedException_For_Incorrect_MessageEntityType()
    {
        MessageEntity messageEntity = new MessageEntity() { Type = (MessageEntityType)int.MaxValue };

        // ToDo: add MessageEntityType.Unknown ?
        //    protected override string GetStringValue(MessageEntityType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        NotSupportedException ex = Assert.Throws<NotSupportedException>(() =>
            JsonConvert.SerializeObject(messageEntity));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    private class MessageEntity
    {
        [JsonProperty(Required = Required.Always)]
        public MessageEntityType Type { get; init; }
    }
}
