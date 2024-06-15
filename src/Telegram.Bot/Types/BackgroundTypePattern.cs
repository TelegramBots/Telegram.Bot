using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// The background is a PNG or TGV (gzipped subset of SVG with MIME type “application/x-tgwallpattern”) pattern
/// to be combined with the background fill chosen by the user.
/// </summary>
public class BackgroundTypePattern : BackgroundType
{
    /// <inheritdoc />
    public override BackgroundTypeKind Type => BackgroundTypeKind.Pattern;

    /// <summary>
    /// Document with the pattern
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required Document Document { get; set; }

    /// <summary>
    /// The background fill that is combined with the pattern
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required BackgroundFill Fill { get; set; }

    /// <summary>
    /// Intensity of the pattern when it is shown above the filled background; 0-100
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int Intensity { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the background fill must be applied only to the pattern itself.
    /// All other pixels are black in this case. For dark themes only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsInverted { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the background moves slightly when the device is tilted
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsMoving { get; set; }
}
