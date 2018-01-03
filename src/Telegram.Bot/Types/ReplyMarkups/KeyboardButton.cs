using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups
{
    /// <summary>
    /// This object represents one button of the reply keyboard. For simple text buttons String can be used instead of this object to specify text of the button.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class KeyboardButton : IKeyboardButton
    {
        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string Text { get; set; }

        /// <summary>
        /// If <c>true</c>, the user's current location will be sent when the button is pressed. Available in private chats only
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool RequestLocation { get; set; }

        /// <summary>
        /// If <c>true</c>, the user's phone number will be sent as a contact when the button is pressed. Available in private chats only
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool RequestContact { get; set; }

        /// <summary>
        /// Initializes a new <see cref="KeyboardButton"/>
        /// </summary>
        public KeyboardButton()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        public KeyboardButton(string text)
        {
            Text = text;
        }

        public static KeyboardButton WithRequestContact(string text) =>
            new KeyboardButton(text) {RequestContact = true};

        public static KeyboardButton WithRequestLocation(string text) =>
            new KeyboardButton(text) {RequestLocation = true};

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