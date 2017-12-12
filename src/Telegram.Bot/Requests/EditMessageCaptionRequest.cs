using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Edit captions and game messages sent by the bot. On success the edited <see cref="Message"/> is returned.
    /// </summary>
    public class EditMessageCaptionRequest : RequestBase<Message>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Identifier of the sent message
        /// </summary>
        public int MessageId { get; set; }

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
        public EditMessageCaptionRequest()
            : base("editMessageCaption")
        { }

        /// <summary>
        /// Initializes a new request with chatId, messageId and new caption
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="messageId">Identifier of the sent message</param>
        /// <param name="caption">New caption of the message</param>
        public EditMessageCaptionRequest(ChatId chatId, int messageId, string caption)
            : this()
        {
            ChatId = chatId;
            MessageId = messageId;
            Caption = caption;
        }

        /// <summary>
        /// Initializes a new request with chatId, messageId, new caption and new inline keyboard
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="messageId">Identifier of the sent message</param>
        /// <param name="caption">New caption of the message</param>
        /// <param name="replyMarkup">New inline keyboard of the sent message</param>
        public EditMessageCaptionRequest(
            ChatId chatId,
            int messageId,
            string caption,
            InlineKeyboardMarkup replyMarkup)
            : this(chatId, messageId, caption)
        {
            ReplyMarkup = replyMarkup;
        }
    }
}
