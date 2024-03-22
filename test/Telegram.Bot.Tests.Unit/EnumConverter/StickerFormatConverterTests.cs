using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class StickerFormatConverterTests
{
    [Fact]
    public void Should_Verify_All_StickerFormat_Members()
    {
        List<string> stickerFormatMembers = Enum
            .GetNames(typeof(StickerFormat))
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

        string result = JsonSerializer.Serialize(sticker, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(StickerFormatData))]
    public void Should_Convert_String_To_StickerFormat(StickerFormat stickerFormat, string value)
    {
        Sticker expectedResult = new() { Format = stickerFormat };
        string jsonData = @$"{{""format"":""{value}""}}";

        Sticker result = JsonSerializer.Deserialize<Sticker>(jsonData, JsonSerializerOptionsProvider.Options)!;

        Assert.Equal(expectedResult.Format, result.Format);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_StickerFormat()
    {
        string jsonData = @$"{{""format"":""{int.MaxValue}""}}";

        Sticker result = JsonSerializer.Deserialize<Sticker>(jsonData, JsonSerializerOptionsProvider.Options)!;

        Assert.Equal((StickerFormat)0, result.Format);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_StickerFormat()
    {
        Sticker sticker = new() { Format = (StickerFormat)int.MaxValue };

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(sticker, JsonSerializerOptionsProvider.Options));
    }


    class Sticker
    {
        /// <summary>
        /// Format of the sticker
        /// </summary>
        [JsonRequired]
        public StickerFormat Format { get; set; }
    }

    private class StickerFormatData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [StickerFormat.Static, "static"];
            yield return [StickerFormat.Animated, "animated"];
            yield return [StickerFormat.Video, "video"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
