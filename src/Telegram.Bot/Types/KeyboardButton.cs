using Newtonsoft.Json;
using Telegram.Bot.Types.InlineKeyboardButtons;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one button of the reply keyboard. For simple text buttons String can be used instead of this object to specify text of the button. Optional fields are mutually exclusive.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class KeyboardButton
    {
        /// <summary>
        /// Text of the button. If none of the optional fields are used, it will be sent to the bot as a message when the button is pressed
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Text { get; set; }

        /// <summary>
        /// Optional. If <c>true</c>, the user's phone number will be sent as a contact when the button is pressed. Available in private chats only
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool RequestContact { get; set; }

        /// <summary>
        /// Optional. If <c>true</c>, the user's current location will be sent when the button is pressed. Available in private chats only
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool RequestLocation { get; set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="KeyboardButton"/>.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator KeyboardButton(string key)
            => new KeyboardButton(key);

        /// <summary>
        /// Performs an implicit conversion from <see cref="InlineKeyboardButton"/> to <see cref="KeyboardButton"/>.
        /// </summary>
        /// <param name="button"></param>
        public static implicit operator KeyboardButton(InlineKeyboardButton button)
            => new KeyboardButton(button.Text);

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardButton"/> class.
        /// </summary>
        /// <param name="text">The <see cref="Text"/></param>
        public KeyboardButton(string text)
        {
            Text = text;
        }
    }
}
