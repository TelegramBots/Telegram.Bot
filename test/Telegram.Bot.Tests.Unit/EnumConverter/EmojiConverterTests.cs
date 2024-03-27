using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class EmojiConverterTests
{
    [Theory]
    [ClassData(typeof(EmojieData))]
    public void Should_Convert_Emoji_To_String(Emoji emoji, string value)
    {
        Dice dice = new() { Emoji = emoji };
        string expectedResult = @$"{{""emoji"":""{value}""}}";

        string result = JsonSerializer.Serialize(dice, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(EmojieData))]
    public void Should_Convert_String_To_Emoji(Emoji emoji, string value)
    {
        Dice expectedResult = new() { Emoji = emoji };
        string jsonData = @$"{{""emoji"":""{value}""}}";

        Dice? result = JsonSerializer.Deserialize<Dice>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Emoji, result.Emoji);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_Emoji()
    {
        string jsonData = @$"{{""emoji"":""{int.MaxValue}""}}";

        Dice? result = JsonSerializer.Deserialize<Dice>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((Emoji)0, result.Emoji);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_Emoji()
    {
        Dice dice = new() { Emoji = (Emoji)int.MaxValue };

        // ToDo: add Emoji.Unknown ?
        //    protected override string GetStringValue(Emoji value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(dice, JsonSerializerOptionsProvider.Options));
    }


    class Dice
    {
        [JsonRequired]
        public Emoji Emoji { get; init; }
    }

    private class EmojieData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [Emoji.Dice, @"\uD83C\uDFB2"];           // 🎲
            yield return [Emoji.Darts, @"\uD83C\uDFAF"];          // 🎯
            yield return [Emoji.Basketball, @"\uD83C\uDFC0"];     // 🏀
            yield return [Emoji.Football, @"\u26BD"];              // ⚽
            yield return [Emoji.SlotMachine, @"\uD83C\uDFB0"];    // 🎰
            yield return [Emoji.Bowling, @"\uD83C\uDFB3"];        // 🎳
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
