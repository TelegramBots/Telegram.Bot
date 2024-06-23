namespace Telegram.Bot.Types.Payments;

/// <summary>This object represents information about an order.</summary>
public partial class OrderInfo
{
    /// <summary><em>Optional</em>. User name</summary>
    public string? Name { get; set; }

    /// <summary><em>Optional</em>. User's phone number</summary>
    public string? PhoneNumber { get; set; }

    /// <summary><em>Optional</em>. User email</summary>
    public string? Email { get; set; }

    /// <summary><em>Optional</em>. User shipping address</summary>
    public ShippingAddress? ShippingAddress { get; set; }
}
