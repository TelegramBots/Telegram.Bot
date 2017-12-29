using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents bot API response
    /// </summary>
    /// <typeparam name="TResult">Expected type of operation result</typeparam>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ApiResponse<TResult>
    {
        /// <summary>
        /// Gets a value indicating whether the request was successful.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public bool Ok { get; set; }

        /// <summary>
        /// Gets the result object.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TResult Result { get; set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Contains information about why a request was unsuccessful.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ResponseParameters Parameters { get; set; }
    }
}
