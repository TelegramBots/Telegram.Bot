using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a custom keyboard with reply options (see Introduction to bots for details and examples).
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ReplyKeyboardMarkup : ReplyMarkup
    {
        /// <summary>
        /// Array of button rows, each represented by an Array of KeyboardButton objects
        /// </summary>
        [JsonProperty(PropertyName = "keyboard", Required = Required.Always)]
        public KeyboardButton[][] Keyboard { get; set; }

        /// <summary>
        /// Optional. Requests clients to resize the keyboard vertically for optimal fit (e.g., make the keyboard smaller if there are just two rows of buttons). Defaults to false, in which case the custom keyboard is always of the same height as the app's standard keyboard.
        /// </summary>
        [JsonProperty(PropertyName = "resize_keyboard", Required = Required.Default,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool ResizeKeyboard { get; set; }

        /// <summary>
        /// Optional. Requests clients to hide the keyboard as soon as it's been used. Defaults to false.
        /// </summary>
        [JsonProperty(PropertyName = "one_time_keyboard", Required = Required.Default,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool OneTimeKeyboard { get; set; }

        public ReplyKeyboardMarkup() { }

        public ReplyKeyboardMarkup(KeyboardButton[] keyboardRow, bool resizeKeyboard = false,
            bool oneTimeKeyboard = false)
        {
            Keyboard = new[]
            {
                keyboardRow
            };
            ResizeKeyboard = resizeKeyboard;
            OneTimeKeyboard = oneTimeKeyboard;
        }

        public ReplyKeyboardMarkup(KeyboardButton[][] keyboard, bool resizeKeyboard = false,
            bool oneTimeKeyboard = false)
        {
            Keyboard = keyboard;
            ResizeKeyboard = resizeKeyboard;
            OneTimeKeyboard = oneTimeKeyboard;
        }
    }
}
