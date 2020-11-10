using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a venue.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Venue
    {
        /// <summary>
        /// Venue location
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Location Location { get; set; }

        /// <summary>
        /// Title of the result
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// Address of the venue
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Address { get; set; }

        /// <summary>
        /// Optional. Foursquare identifier of the venue
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FoursquareId { get; set; }


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

        /// <summary>
        /// Optional. Foursquare type of the venue. (For example, "arts_entertainment/default",
        /// "arts_entertainment/aquarium" or "food/icecream".)
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FoursquareType { get; set; }
    }
}
