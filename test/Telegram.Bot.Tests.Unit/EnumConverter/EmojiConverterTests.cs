using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class EmojiConverterTests
{
    [Theory]
    [ClassData(typeof(EmojieData))]
    public void Should_Convert_Emoji_To_String(SendDiceRequest dice, string value)
    {
        string expectedResult = $$"""{"chat_id":1,"emoji":"{{value}}"}""";

        string result = JsonSerializer.Serialize(dice, TelegramBotClientJsonSerializerContext.Instance.SendDiceRequest);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(EmojieData))]
    public void Should_Convert_String_To_Emoji(SendDiceRequest expectedResult, string value)
    {
        string jsonData = $$"""{"chat_id":1,"emoji":"{{value}}"}""";

        SendDiceRequest? result = JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.SendDiceRequest);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Emoji, result.Emoji);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_Emoji()
    {
        Emoji? result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.Emoji);

        Assert.NotNull(result);
        Assert.Equal((Emoji)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_Emoji()
    {
        // ToDo: add Emoji.Unknown ?
        //    protected override string GetStringValue(Emoji value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((Emoji)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.Emoji));
    }

    private class EmojieData : IEnumerable<object[]>
    {
        private static SendDiceRequest NewSendDiceRequest(Emoji emoji)
        {
            return new SendDiceRequest
            {
                ChatId = 1,
                Emoji = emoji,
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewSendDiceRequest(Emoji.Dice), @"\uD83C\uDFB2"];           // 🎲
            yield return [NewSendDiceRequest(Emoji.Darts), @"\uD83C\uDFAF"];          // 🎯
            yield return [NewSendDiceRequest(Emoji.Basketball), @"\uD83C\uDFC0"];     // 🏀
            yield return [NewSendDiceRequest(Emoji.Football), @"\u26BD"];             // ⚽
            yield return [NewSendDiceRequest(Emoji.SlotMachine), @"\uD83C\uDFB0"];    // 🎰
            yield return [NewSendDiceRequest(Emoji.Bowling), @"\uD83C\uDFB3"];        // 🎳
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
