using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class MessageEntityTypeConverterTests
{
    [Fact]
    public void Should_Verify_All_MessageEntityType_Members()
    {
        List<string> messageEntityTypeMembers = Enum
            .GetNames<MessageEntityType>()
            .OrderBy(x => x)
            .ToList();
        List<string> messageEntityDataMembers = new MessageEntityData()
            .Select(x => Enum.GetName(typeof(MessageEntityType), x[0]))
            .OrderBy(x => x)
            .ToList()!;

        Assert.Equal(messageEntityTypeMembers.Count, messageEntityDataMembers.Count);
        Assert.Equal(messageEntityDataMembers, messageEntityTypeMembers);
    }

    [Theory]
    [ClassData(typeof(MessageEntityData))]
    public void Should_Convert_MessageEntityType_To_String(MessageEntityType messageEntityType, string value)
    {
        MessageEntity messageEntity = new() { Type = messageEntityType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(messageEntity);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(MessageEntityData))]
    public void Should_Convert_String_To_MessageEntityType(MessageEntityType messageEntityType, string value)
    {
        MessageEntity expectedResult = new() { Type = messageEntityType };
        string jsonData = @$"{{""type"":""{value}""}}";

        MessageEntity result = JsonConvert.DeserializeObject<MessageEntity>(jsonData)!;

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_MessageEntityType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        MessageEntity result = JsonConvert.DeserializeObject<MessageEntity>(jsonData)!;

        Assert.Equal((MessageEntityType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_NotSupportedException_For_Incorrect_MessageEntityType()
    {
        MessageEntity messageEntity = new() { Type = (MessageEntityType)int.MaxValue };

        Assert.Throws<NotSupportedException>(() => JsonConvert.SerializeObject(messageEntity));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    class MessageEntity
    {
        [JsonProperty(Required = Required.Always)]
        public MessageEntityType Type { get; init; }
    }

    private class MessageEntityData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { MessageEntityType.Mention, "mention" };
            yield return new object[] { MessageEntityType.Hashtag, "hashtag" };
            yield return new object[] { MessageEntityType.BotCommand, "bot_command" };
            yield return new object[] { MessageEntityType.Url, "url" };
            yield return new object[] { MessageEntityType.Email, "email" };
            yield return new object[] { MessageEntityType.Bold, "bold" };
            yield return new object[] { MessageEntityType.Italic, "italic" };
            yield return new object[] { MessageEntityType.Code, "code" };
            yield return new object[] { MessageEntityType.Pre, "pre" };
            yield return new object[] { MessageEntityType.TextLink, "text_link" };
            yield return new object[] { MessageEntityType.TextMention, "text_mention" };
            yield return new object[] { MessageEntityType.PhoneNumber, "phone_number" };
            yield return new object[] { MessageEntityType.Cashtag, "cashtag" };
            yield return new object[] { MessageEntityType.Underline, "underline" };
            yield return new object[] { MessageEntityType.Strikethrough, "strikethrough" };
            yield return new object[] { MessageEntityType.Spoiler, "spoiler" };
            yield return new object[] { MessageEntityType.CustomEmoji, "custom_emoji" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
