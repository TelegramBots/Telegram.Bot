using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send point on the map. On success, the sent <see cref="Message"/> is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SendLocationRequest : RequestBase<Message>, IChatTargetable, IBusinessConnectable
{
    /// <inheritdoc />
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? BusinessConnectionId { get; set; }

    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Latitude of the location
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public required double Latitude { get; init; }

    /// <summary>
    /// Longitude of the location
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public required double Longitude { get; init; }

    /// <summary>
    /// The radius of uncertainty for the location, measured in meters; 0-1500
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public double? HorizontalAccuracy { get; set; }

    /// <summary>
    /// Period in seconds for which the location will be updated, should be between 60 and 86400
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? LivePeriod { get; set; }

    /// <summary>
    /// For live locations, a direction in which the user is moving, in degrees.
    /// Must be between 1 and 360 if specified.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Heading { get; set; }

    /// <summary>
    /// For live locations, a maximum distance for proximity alerts about approaching another
    /// chat member, in meters. Must be between 1 and 100000 if specified.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ProximityAlertRadius { get; set; }

    /// <summary>
    /// Sends the message silently. Users will receive a notification with no sound.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableNotification { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ProtectContent"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ProtectContent { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyParameters"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ReplyParameters? ReplyParameters { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyMarkup"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IReplyMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request with chatId, latitude and longitude
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="latitude">Latitude of the location</param>
    /// <param name="longitude">Longitude of the location</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SendLocationRequest(ChatId chatId, double latitude, double longitude)
        : this()
    {
        ChatId = chatId;
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SendLocationRequest()
        : base("sendLocation")
    { }
}
