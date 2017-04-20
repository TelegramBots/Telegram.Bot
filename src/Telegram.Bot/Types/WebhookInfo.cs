using System;

using Newtonsoft.Json;

using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Contains information about the current status of a webhook.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class WebhookInfo
    {
        /// <summary>
        /// Webhook URL, may be empty if webhook is not set up
        /// </summary>
        [JsonProperty("url", Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// True, if a custom certificate was provided for webhook certificate checks
        /// </summary>
        [JsonProperty("has_custom_certificate", Required = Required.Always)]
        public bool HasCustomCertificate { get; set; }

        /// <summary>
        /// Number of updates awaiting delivery
        /// </summary>
        [JsonProperty("pending_update_count", Required = Required.Always)]
        public int PendingUpdateCount { get; set; }

        /// <summary>
        /// Unix time for the most recent error that happened when trying to deliver an update via webhook
        /// </summary>
        [JsonProperty("last_error_date", Required = Required.Default)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastErrorDate { get; set; }

        /// <summary>
        /// Error message in human-readable format for the most recent error that happened when trying to deliver an update via webhook
        /// </summary>
        [JsonProperty("last_error_message", Required = Required.Default)]
        public string LastErrorMessage { get; set; }

        /// <summary>
        /// Maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery
        /// </summary>
        [JsonProperty("max_connections", Required = Required.Default)]
        public int MaxConnections { get; set; }

        /// <summary>
        /// A list of update types the bot is subscribed to. Defaults to all update types
        /// </summary>
        [JsonProperty("allowed_updates", Required = Required.Default)]
        public UpdateType[] AllowedUpdates { get; set; }
    }
}
