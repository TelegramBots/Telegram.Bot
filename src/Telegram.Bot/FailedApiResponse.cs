using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types;

namespace Telegram.Bot
{
    /// <summary>
    /// Represents bot API response
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    internal class FailedApiResponse
    {
        /// <summary>
        /// Gets a value indicating whether the request was successful.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public bool Ok { get; set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Description { get; set; } = default!;

        /// <summary>
        /// Gets the error code.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Contains information about why a request was unsuccessful.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ResponseParameters? Parameters { get; set; }
    }
}
