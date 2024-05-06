using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

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
            .Select(x => ((MessageEntity)x[0]).Type.ToString()) // Извлекаем формат стикера из объекта InputSticker
            .OrderBy(x => x)
            .ToList();

        Assert.Equal(messageEntityTypeMembers.Count, messageEntityDataMembers.Count);
        Assert.Equal(messageEntityDataMembers, messageEntityTypeMembers);
    }

    [Theory]
    [ClassData(typeof(MessageEntityData))]
    public void Should_Convert_MessageEntityType_To_String(MessageEntity messageEntity, string value)
    {
        string expectedResult = $$"""{"type":"{{value}}","offset":0,"length":10}""";

        string result = JsonSerializer.Serialize(messageEntity, TelegramBotClientJsonSerializerContext.Instance.MessageEntity);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(MessageEntityData))]
    public void Should_Convert_String_To_MessageEntityType(MessageEntity expectedResult, string value)
    {
        string jsonData = $$"""{"type":"{{value}}","offset":0,"length":10}""";

        MessageEntity result = JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.MessageEntity)!;

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_MessageEntityType()
    {
        MessageEntityType result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.MessageEntityType)!;

        Assert.Equal((MessageEntityType)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_MessageEntityType()
    {
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((MessageEntityType)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.MessageEntityType));
    }

    private class MessageEntityData : IEnumerable<object[]>
    {
        private static MessageEntity NewMessageEntity(MessageEntityType messageEntityType)
        {
            return new MessageEntity
            {
                Type = messageEntityType,
                Offset = 0,
                Length = 10,
            };
        }
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewMessageEntity(MessageEntityType.Mention), "mention"];
            yield return [NewMessageEntity(MessageEntityType.Hashtag), "hashtag"];
            yield return [NewMessageEntity(MessageEntityType.BotCommand), "bot_command"];
            yield return [NewMessageEntity(MessageEntityType.Url), "url"];
            yield return [NewMessageEntity(MessageEntityType.Email), "email"];
            yield return [NewMessageEntity(MessageEntityType.Bold), "bold"];
            yield return [NewMessageEntity(MessageEntityType.Italic), "italic"];
            yield return [NewMessageEntity(MessageEntityType.Code), "code"];
            yield return [NewMessageEntity(MessageEntityType.Pre), "pre"];
            yield return [NewMessageEntity(MessageEntityType.TextLink), "text_link"];
            yield return [NewMessageEntity(MessageEntityType.TextMention), "text_mention"];
            yield return [NewMessageEntity(MessageEntityType.PhoneNumber), "phone_number"];
            yield return [NewMessageEntity(MessageEntityType.Cashtag), "cashtag"];
            yield return [NewMessageEntity(MessageEntityType.Underline), "underline"];
            yield return [NewMessageEntity(MessageEntityType.Strikethrough), "strikethrough"];
            yield return [NewMessageEntity(MessageEntityType.Spoiler), "spoiler"];
            yield return [NewMessageEntity(MessageEntityType.CustomEmoji), "custom_emoji"];
            yield return [NewMessageEntity(MessageEntityType.Blockquote), "blockquote"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
