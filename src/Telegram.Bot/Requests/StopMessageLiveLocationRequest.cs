using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Stop updating a live location message sent by the bot
    /// </summary>
    public class StopMessageLiveLocationRequest : RequestBase<Message>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Identifier of the sent message
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public StopMessageLiveLocationRequest()
            : base("stopMessageLiveLocation")
        { }

        /// <summary>
        /// Initializes a new request with chatId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="messageId">Identifier of the sent message</param>
        public StopMessageLiveLocationRequest(ChatId chatId, int messageId)
            : this()
        {
            ChatId = chatId;
            MessageId = messageId;
        }
    }
}
