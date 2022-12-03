using Newtonsoft.Json.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// Contains information about the current status of a webhook.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class WebhookInfo
{
    /// <summary>
    /// Webhook URL, may be empty if webhook is not set up
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Url { get; set; } = default!;

    /// <summary>
    /// <see langword="true"/>, if a custom certificate was provided for webhook certificate checks
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool HasCustomCertificate { get; set; }

    /// <summary>
    /// Number of updates awaiting delivery
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int PendingUpdateCount { get; set; }

    /// <summary>
    /// Optional. Currently used webhook IP address
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Optional. Time for the most recent error that happened when trying to deliver an update via webhook
    /// </summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public DateTime? LastErrorDate { get; set; }

    /// <summary>
    /// Optional. Error message in human-readable format for the most recent error that happened when trying to
    /// deliver an update via webhook
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? LastErrorMessage { get; set; }

    /// <summary>
    /// Optional. Unix time of the most recent error that happened when trying to synchronize available updates
    /// with Telegram datacenters
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? LastSynchronizationErrorDate { get; set; }

    /// <summary>
    /// Optional. Maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MaxConnections { get; set; }

    /// <summary>
    /// Optional. A list of update types the bot is subscribed to. Defaults to all update types except
    /// <see cref="UpdateType.ChatMember"/>
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public UpdateType[]? AllowedUpdates { get; set; }
}
