using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Stop updating a live location message sent via the bot (for inline bots) before live period expires
    /// </summary>
    public class StopInlineMessageLiveLocationRequest : RequestBase<bool>,
                                                        IInlineMessage
    {
        /// <inheritdoc />
        public string InlineMessageId { get; set; }

        /// <summary>
        /// Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public StopInlineMessageLiveLocationRequest()
            : base("stopMessageLiveLocation")
        { }

        /// <summary>
        /// Initializes a new request with inlineMessageId
        /// </summary>
        /// <param name="inlineMessageId">Identifier of the inline message</param>
        public StopInlineMessageLiveLocationRequest(string inlineMessageId)
            : this()
        {
            InlineMessageId = inlineMessageId;
        }
    }
}
