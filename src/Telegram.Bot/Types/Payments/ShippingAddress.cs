namespace Telegram.Bot.Types.Payments;

/// <summary>
/// This object represents a shipping address.
/// </summary>
public partial class ShippingAddress
{
    /// <summary>
    /// Two-letter <a href="https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2</a> country code
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string CountryCode { get; set; } = default!;

    /// <summary>
    /// State, if applicable
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? State { get; set; }

    /// <summary>
    /// City
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string City { get; set; } = default!;

    /// <summary>
    /// First line for the address
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string StreetLine1 { get; set; } = default!;

    /// <summary>
    /// Second line for the address
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string StreetLine2 { get; set; } = default!;

    /// <summary>
    /// Address post code
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PostCode { get; set; } = default!;
}
