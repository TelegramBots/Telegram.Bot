using Newtonsoft.Json;
using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;
using System;

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
        [JsonProperty(Required = Required.Always)]
        public string Url { get; set; }

        /// <summary>
        /// True, if a custom certificate was provided for webhook certificate checks
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public bool HasCustomCertificate { get; set; }

        /// <summary>
        /// Number of updates awaiting delivery
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int PendingUpdateCount { get; set; }

        /// <summary>
        /// Unix time for the most recent error that happened when trying to deliver an update via webhook
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastErrorDate { get; set; }

        /// <summary>
        /// Error message in human-readable format for the most recent error that happened when trying to deliver an update via webhook
        /// </summary>
        [JsonProperty]
        public string LastErrorMessage { get; set; }

        /// <summary>
        /// Maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery
        /// </summary>
        [JsonProperty]
        public int MaxConnections { get; set; }

        /// <summary>
        /// A list of update types the bot is subscribed to. Defaults to all update types
        /// </summary>
        [JsonProperty]
        public UpdateType[] AllowedUpdates { get; set; }
    }
}
