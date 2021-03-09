using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Emoji on which the dice throw animation is based
    /// <remarks>
    /// This enum is used only in the library APIs and is not present in types that are coming from
    /// Telegram servers for compatibility reasons
    /// </remarks>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Emoji
    {
        /// <summary>
        /// Dice. Resulting value is 1-6
        /// </summary>
        [EnumMember(Value = "🎲")]
        Dice,

        /// <summary>
        /// Darts. Resulting value is 1-6
        /// </summary>
        [EnumMember(Value = "🎯")]
        Darts,

        /// <summary>
        /// Basketball. Resulting value is 1-5
        /// </summary>
        [EnumMember(Value = "🏀")]
        Basketball,

        /// <summary>
        /// Football. Resulting value is 1-5
        /// </summary>
        [EnumMember(Value = "⚽")]
        Football,

        /// <summary>
        /// Slot machine. Resulting value is 1-64
        /// </summary>
        [EnumMember(Value = "🎰")]
        SlotMachine

    }
}
