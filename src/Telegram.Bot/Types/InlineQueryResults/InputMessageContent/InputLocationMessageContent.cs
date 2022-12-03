

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents the content of a location message to be sent as the result of an
/// <see cref="InlineQuery">inline query</see>.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InputLocationMessageContent : InputMessageContent
{
    /// <summary>
    /// Latitude of the location in degrees
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public double Latitude { get; }

    /// <summary>
    /// Longitude of the location in degrees
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public double Longitude { get; }

    /// <summary>
    /// Optional. The radius of uncertainty for the location, measured in meters; 0-1500
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public float? HorizontalAccuracy { get; set; }

    /// <summary>
    /// Optional. Period in seconds for which the location can be updated, should be between 60 and 86400.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? LivePeriod { get; set; }

    /// <summary>
    /// Optional. The direction in which user is moving, in degrees; 1-360. For active live locations only.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Heading { get; set; }

    /// <summary>
    /// Optional. Maximum distance for proximity alerts about approaching another chat member,
    /// in meters. For sent live locations only.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ProximityAlertRadius { get; set; }

    /// <summary>
    /// Initializes a new input location message content
    /// </summary>
    /// <param name="latitude">The latitude of the location</param>
    /// <param name="longitude">The longitude of the location</param>
    public InputLocationMessageContent(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
