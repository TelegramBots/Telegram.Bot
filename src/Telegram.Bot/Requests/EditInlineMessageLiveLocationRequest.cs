using Newtonsoft.Json;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Edit live location messages sent via the bot (for inline bots)
    /// </summary>
    public class EditInlineMessageLiveLocationRequest : RequestBase<bool>,
                                                        IInlineMessage
    {
        /// <inheritdoc />
        public string InlineMessageId { get; set; }

        /// <summary>
        /// Latitude of new location
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        /// Longitude of new location
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        /// Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public EditInlineMessageLiveLocationRequest()
            : base("editMessageLiveLocation")
        { }

        /// <summary>
        /// Initializes a new request with inline message id and new location
        /// </summary>
        /// <param name="inlineMessageId">Identifier of the inline message</param>
        /// <param name="latitude">Latitude of new location</param>
        /// <param name="longitude">Longitude of new location</param>
        public EditInlineMessageLiveLocationRequest(string inlineMessageId, float latitude, float longitude)
            : this()
        {
            InlineMessageId = inlineMessageId;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
