// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes the position of a clickable area within a story.</summary>
public partial class StoryAreaPosition
{
    /// <summary>The abscissa of the area's center, as a percentage of the media width</summary>
    [JsonPropertyName("x_percentage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public double XPercentage { get; set; }

    /// <summary>The ordinate of the area's center, as a percentage of the media height</summary>
    [JsonPropertyName("y_percentage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public double YPercentage { get; set; }

    /// <summary>The width of the area's rectangle, as a percentage of the media width</summary>
    [JsonPropertyName("width_percentage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public double WidthPercentage { get; set; }

    /// <summary>The height of the area's rectangle, as a percentage of the media height</summary>
    [JsonPropertyName("height_percentage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public double HeightPercentage { get; set; }

    /// <summary>The clockwise rotation angle of the rectangle, in degrees; 0-360</summary>
    [JsonPropertyName("rotation_angle")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public double RotationAngle { get; set; }

    /// <summary>The radius of the rectangle corner rounding, as a percentage of the media width</summary>
    [JsonPropertyName("corner_radius_percentage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public double CornerRadiusPercentage { get; set; }
}
