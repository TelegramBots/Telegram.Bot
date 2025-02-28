// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary>This object represents information about an order.</summary>
public partial class OrderInfo
{
    /// <summary><em>Optional</em>. User name</summary>
    public string? Name { get; set; }

    /// <summary><em>Optional</em>. User's phone number</summary>
    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    /// <summary><em>Optional</em>. User email</summary>
    public string? Email { get; set; }

    /// <summary><em>Optional</em>. User shipping address</summary>
    [JsonPropertyName("shipping_address")]
    public ShippingAddress? ShippingAddress { get; set; }
}
