using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a venue.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Venue
    {
        /// <summary>
        /// Venue location
        /// </summary>
        [JsonProperty("location", Required = Required.Always)]
        public Location Location { get; set; }

        /// <summary>
        /// Title of the result
        /// </summary>
        [JsonProperty("title", Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// Address of the venue
        /// </summary>
        [JsonProperty("address", Required = Required.Always)]
        public string Address { get; set; }

        /// <summary>
        /// Optional. Foursquare identifier of the venue
        /// </summary>
        [JsonProperty("foursquare_id", Required = Required.Default)]
        public string FoursquareId { get; set; }
    }
}
