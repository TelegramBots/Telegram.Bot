using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a custom inline keyboard with reply options (see Introduction to bots for details and examples).
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineKeyboardMarkup : ReplyMarkup
    {
        //TODO: FIX Array of Array
        /// <summary>
        /// Array of inline button rows, each represented by an Array of Strings
        /// </summary>
        [JsonProperty(PropertyName = "inline_keyboard", Required = Required.Always)]
        public string[][] InlineKeyboard { get; set; }
    }
}
