// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes the physical address of a location.</summary>
public partial class LocationAddress
{
    /// <summary>The two-letter ISO 3166-1 alpha-2 country code of the country where the location is located</summary>
    [JsonPropertyName("country_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string CountryCode { get; set; } = default!;

    /// <summary><em>Optional</em>. State of the location</summary>
    public string? State { get; set; }

    /// <summary><em>Optional</em>. City of the location</summary>
    public string? City { get; set; }

    /// <summary><em>Optional</em>. Street address of the location</summary>
    public string? Street { get; set; }
}
