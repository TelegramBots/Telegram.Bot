namespace Telegram.Bot.Types.Payments;

/// <summary>This object contains information about an incoming shipping query.</summary>
public partial class ShippingQuery
{
    /// <summary>Unique query identifier</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>User who sent the query</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User From { get; set; } = default!;

    /// <summary>Bot-specified invoice payload</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string InvoicePayload { get; set; } = default!;

    /// <summary>User specified shipping address</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ShippingAddress ShippingAddress { get; set; } = default!;
}
