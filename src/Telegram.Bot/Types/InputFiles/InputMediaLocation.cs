// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Represents a location to be sent.</summary>
public partial class InputMediaLocation : InputPollMedia, InputPollOptionMedia
{
    /// <summary>Type of the media, must be <em>location</em></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public InputMediaType Type => InputMediaType.Location;

    /// <summary>Latitude of the location</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Latitude { get; set; }

    /// <summary>Longitude of the location</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required double Longitude { get; set; }

    /// <summary><em>Optional</em>. The radius of uncertainty for the location, measured in meters; 0-1500</summary>
    [JsonPropertyName("horizontal_accuracy")]
    public double? HorizontalAccuracy { get; set; }

    /// <summary>Initializes an instance of <see cref="InputMediaLocation"/></summary>
    /// <param name="latitude">Latitude of the location</param>
    /// <param name="longitude">Longitude of the location</param>
    [SetsRequiredMembers]
    public InputMediaLocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>Instantiates a new <see cref="InputMediaLocation"/></summary>
    public InputMediaLocation() { }
}
