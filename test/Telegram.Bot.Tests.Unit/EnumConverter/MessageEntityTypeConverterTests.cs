using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class MessageEntityTypeConverterTests
{
    [Fact]
    public void Should_Verify_All_MessageEntityType_Members()
    {
        List<string> messageEntityTypeMembers = Enum
            .GetNames(typeof(MessageEntityType))
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

        string result = JsonSerializer.Serialize(messageEntity, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(MessageEntityData))]
    public void Should_Convert_String_To_MessageEntityType(MessageEntityType messageEntityType, string value)
    {
        MessageEntity expectedResult = new() { Type = messageEntityType };
        string jsonData = @$"{{""type"":""{value}""}}";

        MessageEntity result = JsonSerializer.Deserialize<MessageEntity>(jsonData, JsonSerializerOptionsProvider.Options)!;

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_MessageEntityType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        MessageEntity result = JsonSerializer.Deserialize<MessageEntity>(jsonData, JsonSerializerOptionsProvider.Options)!;

        Assert.Equal((MessageEntityType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_MessageEntityType()
    {
        MessageEntity messageEntity = new() { Type = (MessageEntityType)int.MaxValue };

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(messageEntity, JsonSerializerOptionsProvider.Options));
    }


    class MessageEntity
    {
        [JsonRequired]
        public MessageEntityType Type { get; init; }
    }

    private class MessageEntityData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [MessageEntityType.Mention, "mention"];
            yield return [MessageEntityType.Hashtag, "hashtag"];
            yield return [MessageEntityType.BotCommand, "bot_command"];
            yield return [MessageEntityType.Url, "url"];
            yield return [MessageEntityType.Email, "email"];
            yield return [MessageEntityType.Bold, "bold"];
            yield return [MessageEntityType.Italic, "italic"];
            yield return [MessageEntityType.Code, "code"];
            yield return [MessageEntityType.Pre, "pre"];
            yield return [MessageEntityType.TextLink, "text_link"];
            yield return [MessageEntityType.TextMention, "text_mention"];
            yield return [MessageEntityType.PhoneNumber, "phone_number"];
            yield return [MessageEntityType.Cashtag, "cashtag"];
            yield return [MessageEntityType.Underline, "underline"];
            yield return [MessageEntityType.Strikethrough, "strikethrough"];
            yield return [MessageEntityType.Spoiler, "spoiler"];
            yield return [MessageEntityType.CustomEmoji, "custom_emoji"];
            yield return [MessageEntityType.Blockquote, "blockquote"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
