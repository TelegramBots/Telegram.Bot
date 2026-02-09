// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a video file of a specific quality.</summary>
public partial class VideoQuality : FileBase
{
    /// <summary>Video width</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Width { get; set; }

    /// <summary>Video height</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Height { get; set; }

    /// <summary>Codec that was used to encode the video, for example, “h264”, “h265”, or “av01”</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Codec { get; set; } = default!;
}
