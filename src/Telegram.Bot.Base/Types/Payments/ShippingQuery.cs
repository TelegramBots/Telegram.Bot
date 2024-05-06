namespace Telegram.Bot.Types.Payments;

/// <summary>
/// This object contains information about an incoming shipping query.
/// </summary>
public class ShippingQuery
{
    /// <summary>
    /// Unique query identifier
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>
    /// User who sent the query
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User From { get; set; } = default!;

    /// <summary>
    /// Bot specified invoice payload
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string InvoicePayload { get; set; } = default!;

    /// <summary>
    /// User specified shipping address
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ShippingAddress ShippingAddress { get; set; } = default!;
}
