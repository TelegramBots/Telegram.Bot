using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups.Buttons
{
    /// <summary>
    /// This object represents one button of an inline keyboard that sends a callback query to the bot when pressed.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
        NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineKeyboardCallbackButton : InlineKeyboardButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardCallbackButton"/> class.
        /// </summary>
        /// <param name="textAndCallbackData">The label text on the button and callback data.</param>
        public InlineKeyboardCallbackButton(string textAndCallbackData)
            : this(textAndCallbackData, textAndCallbackData)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardCallbackButton"/> class.
        /// </summary>
        /// <param name="text">Label text on the button</param>
        /// <param name="callbackData">The callback data</param>
        public InlineKeyboardCallbackButton(string text, string callbackData)
            : base(text)
        {
            CallbackData = callbackData;
        }

        /// <summary>
        /// Data to be sent in a callback query to the bot when button is pressed
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string CallbackData { get; set; }
    }
}
