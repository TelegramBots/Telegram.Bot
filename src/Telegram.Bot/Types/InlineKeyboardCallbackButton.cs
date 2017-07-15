using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one button of an inline keyboard that sends a callback query to the bot when pressed.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineKeyboardCallbackButton : InlineKeyboardButton
    {
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
