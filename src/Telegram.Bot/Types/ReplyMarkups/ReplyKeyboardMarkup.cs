using Newtonsoft.Json;

namespace Telegram.Bot.Types.ReplyMarkups
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
        [JsonProperty(Required = Required.Always)]
        public KeyboardButton[][] Keyboard { get; set; }

        /// <summary>
        /// Optional. Requests clients to resize the keyboard vertically for optimal fit (e.g., make the keyboard smaller if there are just two rows of <see cref="KeyboardButton"/>). Defaults to <c>false</c>, in which case the custom keyboard is always of the same height as the app's standard keyboard.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool ResizeKeyboard { get; set; }

        /// <summary>
        /// Optional. Requests clients to hide the keyboard as soon as it's been used. Defaults to <c>false</c>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool OneTimeKeyboard { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplyKeyboardMarkup"/> class.
        /// </summary>
        public ReplyKeyboardMarkup() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplyKeyboardMarkup"/> class.
        /// </summary>
        /// <param name="keyboardRow">The keyboard row.</param>
        /// <param name="resizeKeyboard">if set to <c>true</c> the keyboard resizes vertically for optimal fit.</param>
        /// <param name="oneTimeKeyboard">if set to <c>true</c> the client hides the keyboard as soon as it's been used.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplyKeyboardMarkup"/> class.
        /// </summary>
        /// <param name="keyboard">The keyboard.</param>
        /// <param name="resizeKeyboard">if set to <c>true</c> the keyboard resizes vertically for optimal fit.</param>
        /// <param name="oneTimeKeyboard">if set to <c>true</c> the client hides the keyboard as soon as it's been used.</param>
        public ReplyKeyboardMarkup(KeyboardButton[][] keyboard, bool resizeKeyboard = false,
            bool oneTimeKeyboard = false)
        {
            Keyboard = keyboard;
            ResizeKeyboard = resizeKeyboard;
            OneTimeKeyboard = oneTimeKeyboard;
        }
    }
}
