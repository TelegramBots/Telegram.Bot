using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Contains information about why a request was unsuccessful.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ResponseParameters
    {
        /// <summary>
        /// The group has been migrated to a supergroup with the specified identifier.
        /// </summary>
        [JsonProperty("migrate_to_chat_id", Required = Required.Default)]
        public long MigrateToChatId { get; set; }

        /// <summary>
        /// In case of exceeding flood control, the number of seconds left to wait before the request can be repeated.
        /// </summary>
        [JsonProperty("retry_after", Required = Required.Default)]
        public int RetryAfter { get; set; }
    }
}
