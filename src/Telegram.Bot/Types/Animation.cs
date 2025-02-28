// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents an animation file (GIF or H.264/MPEG-4 AVC video without sound).</summary>
public partial class Animation : FileBase
{
    /// <summary>Video width as defined by the sender</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Width { get; set; }

    /// <summary>Video height as defined by the sender</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Height { get; set; }

    /// <summary>Duration of the video in seconds as defined by the sender</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Duration { get; set; }

    /// <summary><em>Optional</em>. Animation thumbnail as defined by the sender</summary>
    public PhotoSize? Thumbnail { get; set; }

    /// <summary><em>Optional</em>. Original animation filename as defined by the sender</summary>
    [JsonPropertyName("file_name")]
    public string? FileName { get; set; }

    /// <summary><em>Optional</em>. MIME type of the file as defined by the sender</summary>
    [JsonPropertyName("mime_type")]
    public string? MimeType { get; set; }
}
