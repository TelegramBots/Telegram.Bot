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
        /// Dice
        /// </summary>
        [EnumMember(Value = "🎲")]
        Dice,

        /// <summary>
        /// Darts
        /// </summary>
        [EnumMember(Value = "🎯")]
        Darts
    }
}
