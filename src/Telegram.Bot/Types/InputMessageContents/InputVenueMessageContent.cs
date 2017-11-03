using Newtonsoft.Json;

namespace Telegram.Bot.Types.InputMessageContents
{
    /// <summary>
    /// Represents the content of a <see cref="Venue"/> message to be sent as the result of an <see cref="InlineQuery"/>.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InputVenueMessageContent : InputMessageContent
    {
        /// <summary>
        /// Latitude of the location in degrees
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public float Latitude { get; set; }

        /// <summary>
        /// Longitude of the location in degrees
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public float Longitude { get; set; }

        /// <summary>
        /// Name of the venue
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// Address of the venue
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Address { get; set; }

        /// <summary>
        /// Optional. Foursquare identifier of the venue, if known
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string FoursquareId { get; set; }
    }
}
