

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents the content of a <see cref="Venue"/> message to be sent as the result of an
/// <see cref="InlineQuery">inline query</see>.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InputVenueMessageContent : InputMessageContent
{
    /// <summary>
    /// Latitude of the venue in degrees
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public double Latitude { get; }

    /// <summary>
    /// Longitude of the venue in degrees
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public double Longitude { get; }

    /// <summary>
    /// Name of the venue
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Title { get; }

    /// <summary>
    /// Address of the venue
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Address { get; }

    /// <summary>
    /// Optional. Foursquare identifier of the venue, if known
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? FoursquareId { get; set; }

    /// <summary>
    /// Optional. Foursquare type of the venue. (For example, “arts_entertainment/default”,
    /// “arts_entertainment/aquarium” or “food/icecream”.)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? FoursquareType { get; set; }

    /// <summary>
    /// Google Places identifier of the venue
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? GooglePlaceId { get; set; }

    /// <summary>
    /// Google Places type of the venue.
    /// <a href="https://developers.google.com/places/web-service/supported_types"/>
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? GooglePlaceType { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="title">The name of the venue</param>
    /// <param name="address">The address of the venue</param>
    /// <param name="latitude">The latitude of the venue</param>
    /// <param name="longitude">The longitude of the venue</param>
    public InputVenueMessageContent(string title, string address, double latitude, double longitude)
    {
        Title = title;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }
}
