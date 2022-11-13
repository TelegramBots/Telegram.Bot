using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class StickerTypeConverterTests
{
    [Fact]
    public void Should_Verify_All_StickerType_Members()
    {
        List<string> stickerTypeMembers = Enum
            .GetNames<StickerType>()
            .OrderBy(x => x)
            .ToList();
        List<string> stickerTypeDataMembers = new StickerTypeData()
            .Select(x => Enum.GetName(typeof(StickerType), x[0]))
            .OrderBy(x => x)
            .ToList()!;

        Assert.Equal(stickerTypeMembers.Count, stickerTypeDataMembers.Count);
        Assert.Equal(stickerTypeMembers, stickerTypeDataMembers);
    }


    [Theory]
    [ClassData(typeof(StickerTypeData))]
    public void Should_Convert_StickerType_To_String(StickerType stickerType, string value)
    {
        Sticker sticker = new() { Type = stickerType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(sticker);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(StickerTypeData))]
    public void Should_Convert_String_To_StickerType(StickerType stickerType, string value)
    {
        Sticker expectedResult = new() { Type = stickerType };
        string jsonData = @$"{{""type"":""{value}""}}";

        Sticker result = JsonConvert.DeserializeObject<Sticker>(jsonData)!;

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_StickerType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        Sticker result = JsonConvert.DeserializeObject<Sticker>(jsonData)!;

        Assert.Equal((StickerType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_NotSupportedException_For_Incorrect_StickerType()
    {
        Sticker sticker = new() { Type = (StickerType)int.MaxValue };

        Assert.Throws<NotSupportedException>(() => JsonConvert.SerializeObject(sticker));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    class Sticker
    {
        /// <summary>
        /// Type of the sticker. The type of the sticker is independent from its format,
        /// which is determined by the fields <see cref="IsAnimated"/> and <see cref="IsVideo"/>.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public StickerType Type { get; set; }
    }

    private class StickerTypeData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { StickerType.Regular, "regular" };
            yield return new object[] { StickerType.Mask, "mask" };
            yield return new object[] { StickerType.CustomEmoji, "custom_emoji" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
