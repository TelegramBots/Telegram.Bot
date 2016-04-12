using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents an inline keyboard button
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineKeyboardButton : ReplyMarkup
    {
        /// <summary>
        /// Label text on the button
        /// </summary>
        [JsonProperty(PropertyName = "text", Required = Required.Always)]
        public string Text { get; set; }

		/// <summary>
		/// HTTP url to be opened when button is pressed
		/// </summary>
		[JsonProperty(PropertyName = "url", Required = Required.Default)]
		public string Url { get; set; }

		/// <summary>
		/// Data to be sent in a callback query to the bot when button is pressed
		/// </summary>
		[JsonProperty(PropertyName = "callback_data", Required = Required.Default)]
		public string CallbackData { get; set; }

		/// <summary>
		/// If set, pressing the button will prompt the user to select one of their chats, open that chat and insert the bot‘s username and the specified inline query in the input field. Can be empty, in which case just the bot’s username will be inserted.
		/// </summary>
		[JsonProperty(PropertyName = "switch_inline_query", Required = Required.Default)]
		public string SwitchInlineQuery { get; set; }
	}
}
