using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class ChatTypeConverterTests
{
    [Theory]
    [ClassData(typeof(ChatData))]
    public void Should_Convert_ChatType_To_String(Chat chat, string value)
    {
        string expectedResult = $$"""{"id":1,"type":"{{value}}"}""";

        string result = JsonSerializer.Serialize(chat, TelegramBotClientJsonSerializerContext.Instance.Chat);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(ChatData))]
    public void Should_Convert_String_To_ChatType(Chat expectedResult, string value)
    {
        string jsonData = $$"""{"id":1,"type":"{{value}}"}""";

        Chat? result = JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.Chat);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatType()
    {
        ChatType? result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.ChatType);

        Assert.NotNull(result);
        Assert.Equal((ChatType)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_ChatType()
    {
        // ToDo: add ChatType.Unknown ?
        //    protected override string GetStringValue(ChatType value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((ChatType)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.ChatType));
    }

    private class ChatData : IEnumerable<object[]>
    {
        private static Chat NewChat(ChatType chatType)
        {
            return new Chat
            {
                Type = chatType,
                Id = 1,
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewChat(ChatType.Private), "private"];
            yield return [NewChat(ChatType.Group), "group"];
            yield return [NewChat(ChatType.Channel), "channel"];
            yield return [NewChat(ChatType.Supergroup), "supergroup"];
            yield return [NewChat(ChatType.Sender), "sender"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
