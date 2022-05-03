using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class Test_EmojiConverter
{
    [Theory]
    [InlineData(Emoji.Dice, "🎲")]
    [InlineData(Emoji.Darts, "🎯")]
    [InlineData(Emoji.Basketball, "🏀")]
    [InlineData(Emoji.Football, "⚽")]
    [InlineData(Emoji.SlotMachine, "🎰")]
    [InlineData(Emoji.Bowling, "🎳")]
    public void Sould_Convert_Emoji_To_String(Emoji emoji, string value)
    {
        Dice dice = new Dice() { Emoji = emoji };
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
    public void Sould_Convert_String_To_Emoji(Emoji emoji, string value)
    {
        Dice expectedResult = new Dice() { Emoji = emoji };
        string jsonData = @$"{{""emoji"":""{value}""}}";

        Dice result = JsonConvert.DeserializeObject<Dice>(jsonData);

        Assert.Equal(expectedResult.Emoji, result.Emoji);
    }

    [Fact]
    public void Sould_Return_Zero_For_Incorrect_Emoji()
    {
        string jsonData = @$"{{""emoji"":""{int.MaxValue}""}}";

        Dice result = JsonConvert.DeserializeObject<Dice>(jsonData);

        Assert.Equal((Emoji)0, result.Emoji);
    }

    [Fact]
    public void Sould_Throw_NotSupportedException_For_Incorrect_Emoji()
    {
        Dice dice = new Dice() { Emoji = (Emoji)int.MaxValue };

        // ToDo: add Emoji.Unknown ?
        //    protected override string GetStringValue(Emoji value) =>
        //        EnumToString.TryGetValue(value, out var stringValue)
        //            ? stringValue
        //            : "unknown";
        NotSupportedException ex = Assert.Throws<NotSupportedException>(() =>
            JsonConvert.SerializeObject(dice));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    private class Dice
    {
        [JsonProperty(Required = Required.Always)]
        public Emoji Emoji { get; init; }
    }
}
