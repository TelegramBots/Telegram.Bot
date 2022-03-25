using Newtonsoft.Json;
using Telegram.Bot.Converters;

namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Emoji on which the dice throw animation is based
/// <remarks>
/// This enum is used only in the library APIs and is not present in types that are coming from
/// Telegram servers for compatibility reasons
/// </remarks>
/// </summary>
[JsonConverter(typeof(EmojiConverter))]
public enum Emoji
{
    /// <summary>
    /// Dice. Resulting value is 1-6
    /// </summary>
    Dice = 1,

    /// <summary>
    /// Darts. Resulting value is 1-6
    /// </summary>
    Darts,

    /// <summary>
    /// Basketball. Resulting value is 1-5
    /// </summary>
    Basketball,

    /// <summary>
    /// Football. Resulting value is 1-5
    /// </summary>
    Football,

    /// <summary>
    /// Slot machine. Resulting value is 1-64
    /// </summary>
    SlotMachine,

    /// <summary>
    /// Bowling. Result value is 1-6
    /// </summary>
    Bowling
}