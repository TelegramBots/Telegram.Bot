#if NETSTANDARD2_0
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
#endif

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents bot API response
    /// </summary>
    /// <typeparam name="TResult">Expected type of operation result</typeparam>
#if !NETSTANDARD2_0
        [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
#endif
    public class ApiResponse<TResult>
    {
        /// <summary>
        /// Gets a value indicating whether the request was successful.
        /// </summary>
#if !NETSTANDARD2_0
        [JsonProperty(Required = Required.Always)]
#endif
        public bool Ok { get; set; }

        /// <summary>
        /// Gets the result object.
        /// </summary>
#if !NETSTANDARD2_0
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
#endif
        public TResult Result { get; set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
#if !NETSTANDARD2_0
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
#endif
        public string Description { get; set; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("error_code")]
#else
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
#endif

        public int ErrorCode { get; set; }

        /// <summary>
        /// Contains information about why a request was unsuccessful.
        /// </summary>
#if !NETSTANDARD2_0
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
#endif
        public ResponseParameters Parameters { get; set; }
    }
}
