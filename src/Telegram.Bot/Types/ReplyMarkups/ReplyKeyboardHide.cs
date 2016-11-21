using Newtonsoft.Json;
using System;

namespace Telegram.Bot.Types.ReplyMarkups
{
    /// <summary>
    /// Upon receiving a message with this object, Telegram clients will hide the current custom keyboard and display the default letter-keyboard. By default, custom keyboards are displayed until a new keyboard is sent by a bot. An exception is made for one-time keyboards that are hidden immediately after the user presses a button (see <see cref="ReplyKeyboardMarkup"/>).
    /// </summary>
    /// <seealso cref="ReplyKeyboardMarkup"/>
    [JsonObject(MemberSerialization.OptIn)]
    [Obsolete("Use ReplyKeyboardRemove")]
    public class ReplyKeyboardHide : ReplyMarkup
    {
        /// <summary>
        /// Requests clients to hide the custom keyboard
        /// </summary>
        [JsonProperty(PropertyName = "remove_keyboard", Required = Required.Always)]
        [Obsolete("Use ReplyKeyboardRemove")]
        public bool HideKeyboard { get; set; } = true;
    }
}
