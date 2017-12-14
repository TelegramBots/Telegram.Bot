using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups.Buttons
{
    /// <summary>
    /// This object represents one button of the reply keyboard. For simple text buttons String can be used instead of this object to specify text of the button.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
        NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class KeyboardButton : IKeyboardButton
    {
        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string Text { get; set; }

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
        /// <param name="text">The texton on the button</param>
        public KeyboardButton(string text)
        {
            Text = text;
        }
    }
}
