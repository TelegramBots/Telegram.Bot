using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Gender, male or female
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum Gender
    {
        /// <summary>
        /// Male
        /// </summary>
        Male,

        /// <summary>
        /// Female
        /// </summary>
        Female
    }
}
