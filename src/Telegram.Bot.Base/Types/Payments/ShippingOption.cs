namespace Telegram.Bot.Types.Payments;

/// <summary>
/// This object represents one shipping option.
/// </summary>
public class ShippingOption
{
    /// <summary>
    /// Shipping option identifier
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Option title
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Title { get; set; } = default!;

    /// <summary>
    /// List of price portions
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public LabeledPrice[] Prices { get; set; } = default!;
}
