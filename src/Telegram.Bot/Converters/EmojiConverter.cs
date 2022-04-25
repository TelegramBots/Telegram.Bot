using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class EmojiConverter : EnumConverter<Emoji>
{
    static readonly IReadOnlyDictionary<string, Emoji> StringToEnum =
        new Dictionary<string, Emoji>
        {
            {"🎲", Emoji.Dice},
            {"🎯", Emoji.Darts},
            {"🏀", Emoji.Basketball},
            {"⚽", Emoji.Football},
            {"🎰", Emoji.SlotMachine},
            {"🎳", Emoji.Bowling}
        };

    static readonly IReadOnlyDictionary<Emoji, string> EnumToString =
        new Dictionary<Emoji, string>
        {
            {Emoji.Dice, "🎲"},
            {Emoji.Darts, "🎯"},
            {Emoji.Basketball, "🏀"},
            {Emoji.Football, "⚽"},
            {Emoji.SlotMachine, "🎰"},
            {Emoji.Bowling, "🎳"}
        };

    protected override Emoji GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(Emoji value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : "unknown";
}