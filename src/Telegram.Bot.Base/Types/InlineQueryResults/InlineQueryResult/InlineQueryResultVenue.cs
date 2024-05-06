using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types.InlineQueryResults;

/// <summary>
/// Represents a venue. By default, the venue will be sent by the user. Alternatively, you can use
/// <see cref="InlineQueryResultVenue.InputMessageContent"/> to send a message with the specified
/// content instead of the venue.
/// </summary>
public class InlineQueryResultVenue : InlineQueryResult
{
    /// <summary>
    /// Type of the result, must be venue
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override InlineQueryResultType Type => InlineQueryResultType.Venue;

    /// <inheritdoc cref="Documentation.Latitude" />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Latitude { get; init; }

    /// <inheritdoc cref="Documentation.Longitude" />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Longitude { get; init; }

    /// <summary>
    /// Title of the venue
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Title { get; init; }

    /// <summary>
    /// Address of the venue
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Address { get; init; }

    /// <summary>
    /// Optional. Foursquare identifier of the venue if known
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FoursquareId { get; set; }

    /// <summary>
    /// Optional. Foursquare type of the venue. (For example, "arts_entertainment/default",
    /// "arts_entertainment/aquarium" or "food/icecream".)
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FoursquareType { get; set; }

    /// <summary>
    /// Google Places identifier of the venue
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GooglePlaceId { get; set; }

    /// <summary>
    /// Google Places type of the venue.
    /// <a href="https://developers.google.com/places/web-service/supported_types"/>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GooglePlaceType { get; set; }

    /// <inheritdoc cref="Documentation.InputMessageContent" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InputMessageContent? InputMessageContent { get; set; }

    /// <inheritdoc cref="Documentation.ThumbnailUrl" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ThumbnailUrl { get; set; }

    /// <inheritdoc cref="Documentation.ThumbnailWidth" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ThumbnailWidth { get; set; }

    /// <inheritdoc cref="Documentation.ThumbnailHeight" />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ThumbnailHeight { get; set; }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    /// <param name="id">Unique identifier of this result</param>
    /// <param name="latitude">Latitude of the location in degrees</param>
    /// <param name="longitude">Longitude of the location in degrees</param>
    /// <param name="title">Title of the result</param>
    /// <param name="address">Address of the venue</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public InlineQueryResultVenue(
        string id,
        double latitude,
        double longitude,
        string title,
        string address) : base(id)
    {
        Latitude = latitude;
        Longitude = longitude;
        Title = title;
        Address = address;
    }

    /// <summary>
    /// Initializes a new inline query result
    /// </summary>
    public InlineQueryResultVenue()
    { }
}
