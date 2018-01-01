using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;
using Telegram.Bot.Types.InputMessageContents;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Represents a venue. By default, the venue will be sent by the user. Alternatively, you can use <see cref="InputMessageContents.InputMessageContent"/> to send a message with the specified content instead of the venue.
    /// </summary>
    /// <remarks>
    /// This will only work in Telegram versions released after 9 April, 2016. Older clients will ignore them.
    /// </remarks>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn,
                NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InlineQueryResultVenue : InlineQueryResult,
                                          IThumbnailInlineQueryResult,
                                          ITitleInlineQueryResult,
                                          IInputMessageContentResult,
                                          ILocationInlineQueryResult
    {
        /// <summary>
        /// Initializes a new inline query result
        /// </summary>
        public InlineQueryResultVenue(
            string id,
            float latitude,
            float longitude,
            string title,
            string address)
            : base(id, InlineQueryResultType.Venue)
        {
            Latitude = latitude;
            Longitude = longitude;
            Title = title;
            Address = address;
        }

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

        /// <summary>
        /// Optional. Foursquare identifier of the venue if known
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string FoursquareId { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public Uri ThumbUrl { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int ThumbWidth { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int ThumbHeight { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Title { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public InputMessageContent InputMessageContent { get; set; }
    }
}
