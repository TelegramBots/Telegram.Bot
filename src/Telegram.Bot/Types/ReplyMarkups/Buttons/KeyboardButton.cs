using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups.Buttons
{
    /// <summary>
    /// This object represents one button of the reply keyboard. For simple text buttons String can be used instead of this object to specify text of the button.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class KeyboardButton : IKeyboardButton
    {
        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string Text { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        public KeyboardButton(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="KeyboardButton"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator KeyboardButton(string text)
            => new KeyboardButton(text);
    }
}
