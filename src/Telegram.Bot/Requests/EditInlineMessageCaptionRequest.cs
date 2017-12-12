using Newtonsoft.Json;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Edit captions and game messages sent via the bot. On success the edited True is returned.
    /// </summary>
    public class EditInlineMessageCaptionRequest : RequestBase<bool>,
                                                   IInlineMessage
    {
        /// <inheritdoc />
        public string InlineMessageId { get; set; }

        /// <summary>
        /// New caption of the message
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <summary>
        /// A JSON-serialized object for an inline keyboard
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public EditInlineMessageCaptionRequest()
            : base("editMessageCaption")
        { }

        /// <summary>
        /// Initializes a new request with inlineMessageId and new caption
        /// </summary>
        /// <param name="inlineMessageId">InlineMessageId</param>
        /// <param name="caption">New caption of the message</param>
        public EditInlineMessageCaptionRequest(string inlineMessageId, string caption)
            : this()
        {
            InlineMessageId = inlineMessageId;
            Caption = caption;
        }

        /// <summary>
        /// Initializes a new request with inlineMessageId, new caption and new inline keyboard
        /// </summary>
        /// <param name="inlineMessageId">Identifier of the inline message</param>
        /// <param name="caption">New caption of the message</param>
        /// <param name="replyMarkup">New inline keyboard of the sent message</param>
        public EditInlineMessageCaptionRequest(
            string inlineMessageId,
            string caption,
            InlineKeyboardMarkup replyMarkup)
            : this(inlineMessageId, caption)
        {
            ReplyMarkup = replyMarkup;
        }
    }
}
