using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to edit live location messages. A location can be edited until its
/// <see cref="Types.Location.LivePeriod"/> expires or editing is explicitly disabled by a call to
/// <see cref="StopInlineMessageLiveLocationRequest"/>. On success <see langword="true"/> is returned.
/// </summary>
public class EditInlineMessageLiveLocationRequest : RequestBase<bool>
{
    /// <inheritdoc cref="Abstractions.Documentation.InlineMessageId"/>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineMessageId { get; init; }

    /// <summary>
    /// Latitude of new location
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Latitude { get; init; }

    /// <summary>
    /// Longitude of new location
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Longitude { get; init; }

    /// <summary>
    /// The radius of uncertainty for the location, measured in meters; 0-1500
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? HorizontalAccuracy { get; set; }

    /// <summary>
    /// Direction in which the user is moving, in degrees. Must be between 1 and 360 if specified.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Heading { get; set; }

    /// <summary>
    /// Maximum distance for proximity alerts about approaching another chat member, in meters. Must be
    /// between 1 and 100000 if specified.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ProximityAlertRadius { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyMarkup"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request with inlineMessageId, latitude and longitude
    /// </summary>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="latitude">Latitude of new location</param>
    /// <param name="longitude">Longitude of new location</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public EditInlineMessageLiveLocationRequest(string inlineMessageId, double latitude, double longitude)
        : this()
    {
        InlineMessageId = inlineMessageId;
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public EditInlineMessageLiveLocationRequest()
        : base("editMessageLiveLocation")
    { }
}
