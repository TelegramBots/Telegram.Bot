namespace Telegram.Bot.Types;

/// <summary>Describes the current status of a webhook.</summary>
public partial class WebhookInfo
{
    /// <summary>Webhook URL, may be empty if webhook is not set up</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Url { get; set; } = default!;

    /// <summary><see langword="true"/>, if a custom certificate was provided for webhook certificate checks</summary>
    public bool HasCustomCertificate { get; set; }

    /// <summary>Number of updates awaiting delivery</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int PendingUpdateCount { get; set; }

    /// <summary><em>Optional</em>. Currently used webhook IP address</summary>
    public string? IpAddress { get; set; }

    /// <summary><em>Optional</em>. DateTime for the most recent error that happened when trying to deliver an update via webhook</summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? LastErrorDate { get; set; }

    /// <summary><em>Optional</em>. Error message in human-readable format for the most recent error that happened when trying to deliver an update via webhook</summary>
    public string? LastErrorMessage { get; set; }

    /// <summary><em>Optional</em>. DateTime of the most recent error that happened when trying to synchronize available updates with Telegram datacenters</summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? LastSynchronizationErrorDate { get; set; }

    /// <summary><em>Optional</em>. The maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery</summary>
    public int? MaxConnections { get; set; }

    /// <summary><em>Optional</em>. A list of update types the bot is subscribed to. Defaults to all update types except <em>ChatMember</em></summary>
    public UpdateType[]? AllowedUpdates { get; set; }
}
