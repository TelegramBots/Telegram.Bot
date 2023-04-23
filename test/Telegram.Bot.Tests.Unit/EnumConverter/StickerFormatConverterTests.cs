using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class StickerFormatConverterTests
{
    [Fact]
    public void Should_Verify_All_StickerFormat_Members()
    {
        List<string> stickerFormatMembers = Enum
            .GetNames<StickerFormat>()
            .OrderBy(x => x)
            .ToList();
        List<string> stickerFormatDataMembers = new StickerFormatData()
            .Select(x => Enum.GetName(typeof(StickerFormat), x[0]))
            .OrderBy(x => x)
            .ToList()!;

        Assert.Equal(stickerFormatMembers.Count, stickerFormatDataMembers.Count);
        Assert.Equal(stickerFormatMembers, stickerFormatDataMembers);
    }


    [Theory]
    [ClassData(typeof(StickerFormatData))]
    public void Should_Convert_StickerFormat_To_String(StickerFormat stickerFormat, string value)
    {
        Sticker sticker = new() { Format = stickerFormat };
        string expectedResult = @$"{{""format"":""{value}""}}";

        string result = JsonConvert.SerializeObject(sticker);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(StickerFormatData))]
    public void Should_Convert_String_To_StickerFormat(StickerFormat stickerFormat, string value)
    {
        Sticker expectedResult = new() { Format = stickerFormat };
        string jsonData = @$"{{""format"":""{value}""}}";

        Sticker result = JsonConvert.DeserializeObject<Sticker>(jsonData)!;

        Assert.Equal(expectedResult.Format, result.Format);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_StickerFormat()
    {
        string jsonData = @$"{{""format"":""{int.MaxValue}""}}";

        Sticker result = JsonConvert.DeserializeObject<Sticker>(jsonData)!;

        Assert.Equal((StickerFormat)0, result.Format);
    }

    [Fact]
    public void Should_Throw_NotSupportedException_For_Incorrect_StickerFormat()
    {
        Sticker sticker = new() { Format = (StickerFormat)int.MaxValue };

        Assert.Throws<NotSupportedException>(() => JsonConvert.SerializeObject(sticker));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    class Sticker
    {
        /// <summary>
        /// Format of the sticker
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public StickerFormat Format { get; set; }
    }

    private class StickerFormatData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { StickerFormat.Static, "static" };
            yield return new object[] { StickerFormat.Animated, "animated" };
            yield return new object[] { StickerFormat.Video, "video" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
