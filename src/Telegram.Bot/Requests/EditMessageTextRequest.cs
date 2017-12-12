using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Edit text and game messages sent by the bot. On success the edited <see cref="Message"/> is returned.
    /// </summary>
    public class EditMessageTextRequest : RequestBase<Message>
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
        /// New text of the message
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message
        /// </summary>
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
        public EditMessageTextRequest()
            : base("editMessageText")
        { }

        /// <summary>
        /// Initializes a new request with chatId, messageId and new text
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="messageId">Identifier of the sent message</param>
        /// <param name="text">New text of the message</param>
        public EditMessageTextRequest(ChatId chatId, int messageId, string text)
            : this()
        {
            ChatId = chatId;
            MessageId = messageId;
            Text = text;
        }
    }
}
