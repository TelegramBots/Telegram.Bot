

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a location on a map. By default, the location will be sent by the user. Alternatively,
/// you can use <see cref="InlineQueryResultLocation.InputMessageContent"/> to send a message with
/// the specified content instead of the location.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class InlineQueryResultLocation : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be location
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public override InlineQueryResultType Type => InlineQueryResultType.Location;

    /// <inheritdoc cref="Documentation.Latitude" />
    [JsonProperty(Required = Required.Always)]
    public double Latitude { get; }

    /// <inheritdoc cref="Documentation.Longitude" />
    [JsonProperty(Required = Required.Always)]
    public double Longitude { get; }

    /// <summary>
    /// Location title
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Title { get; }

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
    /// Optional. For live locations, a direction in which the user is moving, in degrees.
    /// Must be between 1 and 360 if specified.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Heading { get; set; }

    /// <summary>
    /// Optional. For live locations, a maximum distance for proximity alerts about approaching
    /// another chat member, in meters. Must be between 1 and 100000 if specified.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ProximityAlertRadius { get; set; }

    /// <inheritdoc cref="Documentation.InputMessageContent" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <inheritdoc cref="Documentation.ThumbUrl" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? ThumbUrl { get; set; }

    /// <inheritdoc cref="Documentation.ThumbWidth" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ThumbWidth { get; set; }

    /// <inheritdoc cref="Documentation.ThumbHeight" />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ThumbHeight { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="latitude">Latitude of the location in degrees</param>
    /// <param name="longitude">Longitude of the location in degrees</param>
    /// <param name="title">Title of the result</param>
    public InlineQueryResultLocation(string id, double latitude, double longitude, string title)
        : base(id)
    {
        Latitude = latitude;
        Longitude = longitude;
        Title = title;
    }
}
