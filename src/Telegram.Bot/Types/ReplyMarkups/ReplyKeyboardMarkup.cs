using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups
{
    /// <summary>
    /// This object represents a custom keyboard with reply options (see Introduction to bots for details and examples).
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ReplyKeyboardMarkup : ReplyMarkupBase
    {
        /// <summary>
        /// Array of button rows, each represented by an Array of KeyboardButton objects
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public IEnumerable<IEnumerable<KeyboardButton>> Keyboard { get; set; }

        /// <summary>
        /// Optional. Requests clients to resize the keyboard vertically for optimal fit (e.g., make the keyboard smaller if there are just two rows of <see cref="KeyboardButton"/>). Defaults to <c>false</c>, in which case the custom keyboard is always of the same height as the app's standard keyboard.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool ResizeKeyboard { get; set; }

        /// <summary>
        /// Optional. Requests clients to hide the keyboard as soon as it's been used. Defaults to <c>false</c>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool OneTimeKeyboard { get; set; }

        public ReplyKeyboardMarkup()
        {
        }

        public ReplyKeyboardMarkup(KeyboardButton button)
            : this(new[] {button})
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplyKeyboardMarkup"/> class.
        /// </summary>
        /// <param name="keyboardRow">The keyboard row.</param>
        /// <param name="resizeKeyboard">if set to <c>true</c> the keyboard resizes vertically for optimal fit.</param>
        /// <param name="oneTimeKeyboard">if set to <c>true</c> the client hides the keyboard as soon as it's been used.</param>
        public ReplyKeyboardMarkup(IEnumerable<KeyboardButton> keyboardRow, bool resizeKeyboard = default,
            bool oneTimeKeyboard = default)
            : this(new[] {keyboardRow}, resizeKeyboard, oneTimeKeyboard)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplyKeyboardMarkup"/> class.
        /// </summary>
        /// <param name="keyboard">The keyboard.</param>
        /// <param name="resizeKeyboard">if set to <c>true</c> the keyboard resizes vertically for optimal fit.</param>
        /// <param name="oneTimeKeyboard">if set to <c>true</c> the client hides the keyboard as soon as it's been used.</param>
        public ReplyKeyboardMarkup(IEnumerable<IEnumerable<KeyboardButton>> keyboard, bool resizeKeyboard = default,
            bool oneTimeKeyboard = default)
        {
            Keyboard = keyboard;
            ResizeKeyboard = resizeKeyboard;
            OneTimeKeyboard = oneTimeKeyboard;
        }

        public static implicit operator ReplyKeyboardMarkup(string text) =>
            text is default
                ? default
                : new ReplyKeyboardMarkup(new[] {new KeyboardButton(text)});

        public static implicit operator ReplyKeyboardMarkup(string[] texts) =>
            texts is default
                ? default
                : new[] {texts};

        public static implicit operator ReplyKeyboardMarkup(string[][] textsItems) =>
            textsItems is default
                ? default
                : new ReplyKeyboardMarkup(
                    textsItems.Select(texts =>
                        texts.Select(t => new KeyboardButton(t))
                    ));
    }
}