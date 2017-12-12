using Newtonsoft.Json;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Edit text and game messages sent via the bot (for inline bots). On success True is returned.
    /// </summary>
    public class EditInlineMessageTextRequest : RequestBase<bool>,
                                                IFormattableMessage,
                                                IInlineMessage
    {
        /// <inheritdoc />
        public string InlineMessageId { get; set; }

        /// <summary>
        /// New text of the message
        /// </summary>
        public string Text { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ParseMode ParseMode { get; set; }

        /// <summary>
        /// Disables link previews for links in this message
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableWebPagePreview { get; set; }

        /// <summary>
        /// A JSON-serialized object for an inline keyboard
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public EditInlineMessageTextRequest()
            : base("editMessageText")
        { }

        /// <summary>
        /// Initializes a new request with inlineMessageId and new text
        /// </summary>
        /// <param name="inlineMessageId"></param>
        /// <param name="text">New text of the message</param>
        public EditInlineMessageTextRequest(string inlineMessageId, string text)
            : this()
        {
            InlineMessageId = inlineMessageId;
            Text = text;
        }
    }
}
