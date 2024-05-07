using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

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
            .Select(x => ((InputSticker)x[0]).Format.ToString())
            .OrderBy(x => x)
            .ToList();

        Assert.Equal(stickerFormatMembers.Count, stickerFormatDataMembers.Count);
        Assert.Equal(stickerFormatMembers, stickerFormatDataMembers);
    }


    [Theory]
    [ClassData(typeof(StickerFormatData))]
    public void Should_Convert_StickerFormat_To_String(InputSticker sticker, string value)
    {
        string expectedResult = $$"""{"sticker":"1","format":"{{value}}","emoji_list":["\uD83D\uDE00"]}""";

        string result = JsonSerializer.Serialize(sticker, TelegramBotClientJsonSerializerContext.Instance.InputSticker);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [ClassData(typeof(StickerFormatData))]
    public void Should_Convert_String_To_StickerFormat(InputSticker expectedResult, string value)
    {
        string jsonData = $$"""{"sticker":"1","format":"{{value}}","emoji_list":["\uD83D\uDE00"]}""";

        InputSticker result = JsonSerializer.Deserialize(jsonData, TelegramBotClientJsonSerializerContext.Instance.InputSticker)!;

        Assert.Equal(expectedResult.Format, result.Format);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_StickerFormat()
    {
        StickerFormat result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.StickerFormat)!;

        Assert.Equal((StickerFormat)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_StickerFormat()
    {
        InputSticker sticker = StickerFormatData.NewInputSticker((StickerFormat)int.MaxValue);

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(sticker, TelegramBotClientJsonSerializerContext.Instance.InputSticker));
    }

    private class StickerFormatData : IEnumerable<object[]>
    {
        internal static InputSticker NewInputSticker(StickerFormat stickerFormat)
        {
            return new InputSticker
            {
                Format = stickerFormat,

                Sticker = new InputFileId
                {
                    Id = "1"
                },
                EmojiList = new[] { "😀" }
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewInputSticker(StickerFormat.Static), "static"];
            yield return [NewInputSticker(StickerFormat.Animated), "animated"];
            yield return [NewInputSticker(StickerFormat.Video), "video"];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
