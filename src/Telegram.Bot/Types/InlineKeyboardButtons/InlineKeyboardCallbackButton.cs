using Newtonsoft.Json;

namespace Telegram.Bot.Types.InlineKeyboardButtons
{
    /// <summary>
    /// This object represents one button of an inline keyboard that sends a callback query to the bot when pressed.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineKeyboardCallbackButton : InlineKeyboardButton
    {
        /// <summary>
        /// Optional. Data to be sent in a callback query to the bot when button is pressed
        /// </summary>
        [JsonProperty]
        public string CallbackData { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="callbackData">The callback data.</param>
        public InlineKeyboardCallbackButton(string text, string callbackData) : base(text)
        {
            CallbackData = callbackData;
        }
    }
}
