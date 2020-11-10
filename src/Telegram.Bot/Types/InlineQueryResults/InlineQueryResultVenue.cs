using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a venue. By default, the venue will be sent by the user. Alternatively, you can use <see cref="InputMessageContent"/> to send a message with the specified content instead of the venue.
    /// </summary>
    /// <remarks>
    /// This will only work in Telegram versions released after 9 April, 2016. Older clients will ignore them.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultVenue : InlineQueryResultBase,
        IThumbnailInlineQueryResult,
        ITitleInlineQueryResult,
        IInputMessageContentResult,
        ILocationInlineQueryResult
    {
        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public float Latitude { get; set; }

        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public float Longitude { get; set; }

        /// <summary>
        /// Address of the venue
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Address { get; set; }

        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// Optional. Foursquare identifier of the venue if known
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FoursquareId { get; set; }

        /// <summary>
        /// Optional. Foursquare type of the venue. (For example, "arts_entertainment/default",
        /// "arts_entertainment/aquarium" or "food/icecream".)
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FoursquareType { get; set; }

        /// <summary>
        /// Google Places identifier of the venue
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string GooglePlaceId { get; set; }

        /// <summary>
        /// Google Places type of the venue.
        /// <see cref="https://developers.google.com/places/web-service/supported_types"/>
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string GooglePlaceType { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ThumbUrl { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ThumbWidth { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ThumbHeight { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMessageContentBase InputMessageContent { get; set; }

        private InlineQueryResultVenue()
            : base(InlineQueryResultType.Venue)
        {
        }

        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        /// <param name="id">Unique identifier of this result</param>
        /// <param name="latitude">Latitude of the location in degrees</param>
        /// <param name="longitude">Longitude of the location in degrees</param>
        /// <param name="title">Title of the result</param>
        /// <param name="address">Address of the venue</param>
        public InlineQueryResultVenue(
            string id,
            float latitude,
            float longitude,
            string title,
            string address)
            : base(InlineQueryResultType.Venue, id)
        {
            Latitude = latitude;
            Longitude = longitude;
            Title = title;
            Address = address;
        }
    }
}
