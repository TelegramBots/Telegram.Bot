using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Upon receiving a message with this object, Telegram clients will hide the current custom keyboard and display the default letter-keyboard. By default, custom keyboards are displayed until a new keyboard is sent by a bot. An exception is made for one-time keyboards that are hidden immediately after the user presses a button (see ReplyKeyboardMarkup).
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ReplyKeyboardHide : ReplyMarkup
    {
        /// <summary>
        /// Requests clients to hide the custom keyboard
        /// </summary>
        [JsonProperty(PropertyName = "hide_keyboard", Required = Required.Always)]
        public bool HideKeyboard { get; set; }
    }
}
