using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send information about a venue
    /// </summary>
    public class SendVenueRequest : RequestBase<Message>,
                                    INotifiableMessage,
                                    IReplyMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Latitude of the venue
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        /// Longitude of the venue
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        /// Name of the venue
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Address of the venue
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Foursquare identifier of the venue
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FoursquareId { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ReplyToMessageId { get; set; }

        /// <summary>
        /// Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public SendVenueRequest()
            : base("sendVenue")
        { }

        /// <summary>
        /// Initializes a new request with chatId, location, venue title and address
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="latitude">Latitude of the venue</param>
        /// <param name="longitude">Longitude of the venue</param>
        /// <param name="title">Name of the venue</param>
        /// <param name="address">Address of the venue</param>
        public SendVenueRequest(
            ChatId chatId,
            float latitude,
            float longitude,
            string title,
            string address)
            : this()
        {
            ChatId = chatId;
            Latitude = latitude;
            Longitude = longitude;
            Title = title;
            Address = address;
        }
    }
}
