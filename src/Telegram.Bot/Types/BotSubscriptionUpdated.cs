// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains information about changes to a user payment subscription toward the current bot.</summary>
public partial class BotSubscriptionUpdated
{
    /// <summary>User who subscribed for payments toward the bot</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User User { get; set; } = default!;

    /// <summary>Bot-specified invoice payload</summary>
    [JsonPropertyName("invoice_payload")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string InvoicePayload { get; set; } = default!;

    /// <summary>The new state of the subscription. Currently, it can be one of “canceled” if the user canceled the subscription, “active” if the user re-enabled a previously canceled subscription, or “failed” if payment for the subscription failed.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string State { get; set; } = default!;
}
