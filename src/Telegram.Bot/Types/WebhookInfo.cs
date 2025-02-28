// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes the current status of a webhook.</summary>
public partial class WebhookInfo
{
    /// <summary>Webhook URL, may be empty if webhook is not set up</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Url { get; set; } = default!;

    /// <summary><see langword="true"/>, if a custom certificate was provided for webhook certificate checks</summary>
    [JsonPropertyName("has_custom_certificate")]
    public bool HasCustomCertificate { get; set; }

    /// <summary>Number of updates awaiting delivery</summary>
    [JsonPropertyName("pending_update_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int PendingUpdateCount { get; set; }

    /// <summary><em>Optional</em>. Currently used webhook IP address</summary>
    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; set; }

    /// <summary><em>Optional</em>. DateTime for the most recent error that happened when trying to deliver an update via webhook</summary>
    [JsonPropertyName("last_error_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? LastErrorDate { get; set; }

    /// <summary><em>Optional</em>. Error message in human-readable format for the most recent error that happened when trying to deliver an update via webhook</summary>
    [JsonPropertyName("last_error_message")]
    public string? LastErrorMessage { get; set; }

    /// <summary><em>Optional</em>. DateTime of the most recent error that happened when trying to synchronize available updates with Telegram datacenters</summary>
    [JsonPropertyName("last_synchronization_error_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? LastSynchronizationErrorDate { get; set; }

    /// <summary><em>Optional</em>. The maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery</summary>
    [JsonPropertyName("max_connections")]
    public int? MaxConnections { get; set; }

    /// <summary><em>Optional</em>. A list of update types the bot is subscribed to. Defaults to all update types except <em>ChatMember</em></summary>
    [JsonPropertyName("allowed_updates")]
    public UpdateType[]? AllowedUpdates { get; set; }
}
