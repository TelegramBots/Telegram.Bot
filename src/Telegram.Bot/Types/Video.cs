// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a video file.</summary>
public partial class Video : FileBase
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

    /// <summary><em>Optional</em>. Video thumbnail</summary>
    public PhotoSize? Thumbnail { get; set; }

    /// <summary><em>Optional</em>. Available sizes of the cover of the video in the message</summary>
    public PhotoSize[]? Cover { get; set; }

    /// <summary><em>Optional</em>. Timestamp in seconds from which the video will play in the message</summary>
    [JsonPropertyName("start_timestamp")]
    public int? StartTimestamp { get; set; }

    /// <summary><em>Optional</em>. Original filename as defined by the sender</summary>
    [JsonPropertyName("file_name")]
    public string? FileName { get; set; }

    /// <summary><em>Optional</em>. MIME type of the file as defined by the sender</summary>
    [JsonPropertyName("mime_type")]
    public string? MimeType { get; set; }
}
