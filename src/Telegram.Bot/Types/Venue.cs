namespace Telegram.Bot.Types;

/// <summary>This object represents a venue.</summary>
public partial class Venue
{
    /// <summary>Venue location. Can't be a live location</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Location Location { get; set; } = default!;

    /// <summary>Name of the venue</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Title { get; set; } = default!;

    /// <summary>Address of the venue</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Address { get; set; } = default!;

    /// <summary><em>Optional</em>. Foursquare identifier of the venue</summary>
    public string? FoursquareId { get; set; }

    /// <summary><em>Optional</em>. Foursquare type of the venue. (For example, “arts_entertainment/default”, “arts_entertainment/aquarium” or “food/icecream”.)</summary>
    public string? FoursquareType { get; set; }

    /// <summary><em>Optional</em>. Google Places identifier of the venue</summary>
    public string? GooglePlaceId { get; set; }

    /// <summary><em>Optional</em>. Google Places type of the venue. (See <a href="https://developers.google.com/places/web-service/supported_types">supported types</a>.)</summary>
    public string? GooglePlaceType { get; set; }
}
