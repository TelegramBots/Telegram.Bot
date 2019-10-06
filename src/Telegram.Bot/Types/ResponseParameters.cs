#if NETSTANDARD2_0
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
#endif

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Contains information about why a request was unsuccessful.
    /// </summary>
#if !NETSTANDARD2_0
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
#endif
    public class ResponseParameters
    {
        /// <summary>
        /// The group has been migrated to a supergroup with the specified identifier.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("migrate_to_chat_id")]
#else
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
#endif
        public long MigrateToChatId { get; set; }

        /// <summary>
        /// In case of exceeding flood control, the number of seconds left to wait before the request can be repeated.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("retry_after")]
#else
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
#endif
        public int RetryAfter { get; set; }
    }
}
