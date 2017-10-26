using Newtonsoft.Json;

namespace Telegram.Bot.Types.InputMessageContents
{
    /// <summary>
    /// Represents the content of a location message to be sent as the result of an <see cref="InlineQuery"/>.
    /// </summary>
    /// <remarks>
    /// This will only work in Telegram versions released after 9 April, 2016. Older clients will ignore them.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class InputLocationMessageContent : InputMessageContent
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
        /// Longitude of the location in degrees
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int LivePeriod { get; set; }
    }
}
