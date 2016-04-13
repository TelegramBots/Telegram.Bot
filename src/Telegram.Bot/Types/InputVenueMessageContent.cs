using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents the content of a venue message to be sent as the result of an inline query.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InputVenueMessageContent : InputMessageContent
    {
        /// <summary>
        /// Latitude of the location in degrees
        /// </summary>
        [JsonProperty("latitude", Required = Required.Always)]
        public float Latitude { get; set; }

        /// <summary>
        /// Longitude of the location in degrees
        /// </summary>
        [JsonProperty("longitude", Required = Required.Always)]
        public float Longitude { get; set; }

        /// <summary>
        /// Name of the venue
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// Address of the venue
        /// </summary>
        [JsonProperty("address", Required = Required.Always)]
        public string Address { get; set; }

        /// <summary>
        /// Optional. Foursquare identifier of the venue, if known
        /// </summary>
        [JsonProperty("foursquare_id", Required = Required.Default)]
        public string FoursquareId { get; set; }
    }
}
