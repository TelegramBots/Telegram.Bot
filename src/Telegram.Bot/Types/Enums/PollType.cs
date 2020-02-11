using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// <see cref="Poll"/> type
    /// <remarks>
    /// This enum is used only in the library APIs and is not present in types that are coming from
    /// Telegram servers for compatibility reasons
    /// </remarks>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum PollType
    {
        /// <summary>
        /// Regular poll
        /// </summary>
        Regular,

        /// <summary>
        /// Quiz
        /// </summary>
        Quiz
    }
}
