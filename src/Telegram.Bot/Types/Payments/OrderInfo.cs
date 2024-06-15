namespace Telegram.Bot.Types.Payments;

/// <summary>
/// This object represents information about an order.
/// </summary>
public class OrderInfo
{
    /// <summary>
    /// Optional. User name
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// Optional. User's phone number
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Optional. User email
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Email { get; set; }

    /// <summary>
    /// Optional. User shipping address
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ShippingAddress? ShippingAddress { get; set; }
}
