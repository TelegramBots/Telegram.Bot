using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class StickerTypeConverterTests
{
    // [todo] maybe without TelegramBotClientJsonSerializerContext the whole class

    [Fact]
    public void Should_Verify_All_StickerType_Members()
    {
        List<string> stickerTypeMembers = Enum
            .GetNames(typeof(StickerType))
            .OrderBy(x => x)
            .ToList();
         List<string> stickerTypeDataMembers = new StickerTypeData()
                        .Select(x => ((Sticker)x[0]).Type.ToString()) // Извлекаем тип стикера из объекта Sticker
                        .OrderBy(x => x)
                        .ToList();
        Assert.Equal(stickerTypeMembers.Count, stickerTypeDataMembers.Count);
        Assert.Equal(stickerTypeMembers, stickerTypeDataMembers);
    }


    [Theory]
    [ClassData(typeof(StickerTypeData))]
    public void Should_Convert_StickerType_To_String(Sticker sticker, string value)
    {
        string expectedResult =  $$"""{"type":"{{value}}","width":512,"height":512,"is_animated":false,"is_video":false,"file_id":"1","file_unique_id":"1"}""";

        string result = JsonSerializer.Serialize(sticker, TelegramBotClientJsonSerializerContext.Instance.Sticker);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(StickerTypeData))]
    public void Should_Convert_String_To_StickerType(Sticker expectedResult, string value)
    {
        string jsonData =
            $$"""{"type":"{{value}}","width":512,"height":512,"is_animated":false,"is_video":false,"file_id":"1","file_unique_id":"1"}""";
        Sticker result = JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.Sticker)!;

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_StickerType()
    {
        StickerType result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.StickerType)!;

        Assert.Equal((StickerType)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_StickerType()
    {
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize(
                StickerTypeData.NewSticker((StickerType)int.MaxValue),
                TelegramBotClientJsonSerializerContext.Instance.Sticker));
    }

    private class StickerTypeData : IEnumerable<object[]>
    {
        internal static Sticker NewSticker(StickerType stickerType)
        {
            return new Sticker
            {
                FileId = "1",
                FileUniqueId = "1",
                Type = stickerType,
                Width = 512,
                Height = 512,
                IsAnimated = false,
                IsVideo = false,
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewSticker(StickerType.Regular), "regular"];
            yield return [NewSticker(StickerType.Mask), "mask"];
            yield return [NewSticker(StickerType.CustomEmoji), "custom_emoji"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
