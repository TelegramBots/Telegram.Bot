using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one button of an inline keyboard. You must use exactly one of the optional fields.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineKeyboardButton
    {
        /// <summary>
        /// Label text on the button
        /// </summary>
        [JsonProperty(PropertyName = "text", Required = Required.Always)]
        public string Text { get; set; }

        /// <summary>
        /// Optional. HTTP url to be opened when button is pressed
        /// </summary>
        [JsonProperty(PropertyName = "url", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Url { get; set; }

        /// <summary>
        /// Optional. Data to be sent in a callback query to the bot when button is pressed
        /// </summary>
        [JsonProperty(PropertyName = "callback_data", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string CallbackData { get; set; }

        /// <summary>
        /// Optional. If set, pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified <see cref="InlineQuery"/> in the input field. Can be empty, in which case just the bot's username will be inserted.
        /// </summary>
        /// <remarks>
        /// Note: This offers an easy way for users to start using your bot in inline mode when they are currently in a private chat with it. Especially useful when combined with switchPm[...] parameters (see <see cref="TelegramBotClient.AnswerInlineQueryAsync"/>)  – in this case the user will be automatically returned to the chat they switched from, skipping the chat selection screen.
        /// </remarks>
        [JsonProperty(PropertyName = "switch_inline_query", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string SwitchInlineQuery { get; set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="InlineKeyboardButton"/>.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator InlineKeyboardButton(string key) => new InlineKeyboardButton(key);

        /// <summary>
        /// Performs an implicit conversion from <see cref="KeyboardButton"/> to <see cref="InlineKeyboardButton"/>.
        /// </summary>
        /// <param name="button">The <see cref="KeyboardButton"/></param>
        public static implicit operator InlineKeyboardButton(KeyboardButton button) => new InlineKeyboardButton(button.Text);

        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        public InlineKeyboardButton() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineKeyboardButton"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="callbackData">The callback data.</param>
        public InlineKeyboardButton(string text, string callbackData = null)
        {
            Text = text;
            CallbackData = string.IsNullOrWhiteSpace(callbackData) ? Text : callbackData;
        }
    }
}
