using Newtonsoft.Json;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Edit live location messages sent by the bot
    /// </summary>
    public class EditMessageLiveLocationRequest : RequestBase<Message>,
                                                  IInlineReplyMarkupMessage
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
        /// Latitude of new location
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        /// Longitude of new location
        /// </summary>
        public float Longitude { get; set; }

        /// <inheritdoc cref="IInlineReplyMarkupMessage.ReplyMarkup" />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public EditMessageLiveLocationRequest()
            : base("editMessageLiveLocation")
        { }

        /// <summary>
        /// Initializes a new request with chatId and new location
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="messageId">Identifier of the sent message</param>
        /// <param name="latitude">Latitude of new location</param>
        /// <param name="longitude">Longitude of new location</param>
        public EditMessageLiveLocationRequest(ChatId chatId, int messageId, float latitude, float longitude)
            : this()
        {
            ChatId = chatId;
            MessageId = messageId;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
