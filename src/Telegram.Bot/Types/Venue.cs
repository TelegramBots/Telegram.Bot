namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a venue.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class Venue
{
    /// <summary>
    /// Venue location
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Location Location { get; set; } = default!;

    /// <summary>
    /// Name of the venue
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Title { get; set; } = default!;

    /// <summary>
    /// Address of the venue
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Address { get; set; } = default!;

    /// <summary>
    /// Optional. Foursquare identifier of the venue
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? FoursquareId { get; set; }

    /// <summary>
    /// Optional. Foursquare type of the venue. (For example, "arts_entertainment/default",
    /// "arts_entertainment/aquarium" or "food/icecream".)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? FoursquareType { get; set; }

    /// <summary>
    /// Optional. Google Places identifier of the venue
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? GooglePlaceId { get; set; }

    /// <summary>
    /// Optional. Google Places type of the venue. (See
    /// <a href="https://developers.google.com/places/web-service/supported_types">supported types</a>.)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? GooglePlaceType { get; set; }
}
