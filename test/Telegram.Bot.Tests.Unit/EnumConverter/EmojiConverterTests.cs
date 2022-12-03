using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class EmojiConverterTests
{
    [Theory]
    [InlineData(Emoji.Dice, "🎲")]
    [InlineData(Emoji.Darts, "🎯")]
    [InlineData(Emoji.Basketball, "🏀")]
    [InlineData(Emoji.Football, "⚽")]
    [InlineData(Emoji.SlotMachine, "🎰")]
    [InlineData(Emoji.Bowling, "🎳")]
    public void Should_Convert_Emoji_To_String(Emoji emoji, string value)
    {
        Dice dice = new() { Emoji = emoji };
        string expectedResult = @$"{{""emoji"":""{value}""}}";

        string result = JsonConvert.SerializeObject(dice);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(Emoji.Dice, "🎲")]
    [InlineData(Emoji.Darts, "🎯")]
    [InlineData(Emoji.Basketball, "🏀")]
    [InlineData(Emoji.Football, "⚽")]
    [InlineData(Emoji.SlotMachine, "🎰")]
    [InlineData(Emoji.Bowling, "🎳")]
    public void Should_Convert_String_To_Emoji(Emoji emoji, string value)
    {
        Dice expectedResult = new() { Emoji = emoji };
        string jsonData = @$"{{""emoji"":""{value}""}}";

        Dice? result = JsonConvert.DeserializeObject<Dice>(jsonData);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Emoji, result.Emoji);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_Emoji()
    {
        string jsonData = @$"{{""emoji"":""{int.MaxValue}""}}";

        Dice? result = JsonConvert.DeserializeObject<Dice>(jsonData);

        Assert.NotNull(result);
        Assert.Equal((Emoji)0, result.Emoji);
    }

    [Fact]
    public void Should_Throw_NotSupportedException_For_Incorrect_Emoji()
    {
        Dice dice = new() { Emoji = (Emoji)int.MaxValue };

        // ToDo: add Emoji.Unknown ?
        //    protected override string GetStringValue(Emoji value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        Assert.Throws<NotSupportedException>(() => JsonConvert.SerializeObject(dice));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    class Dice
    {
        [JsonProperty(Required = Required.Always)]
        public Emoji Emoji { get; init; }
    }
}
