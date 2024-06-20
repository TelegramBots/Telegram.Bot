namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a video file.
/// </summary>
public partial class Video : FileBase
{
    /// <summary>
    /// Video width as defined by sender
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Width { get; set; }

    /// <summary>
    /// Video height as defined by sender
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Height { get; set; }

    /// <summary>
    /// Duration of the video in seconds as defined by sender
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Duration { get; set; }

    /// <summary>
    /// <em>Optional</em>. Video thumbnail
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PhotoSize? Thumbnail { get; set; }

    /// <summary>
    /// <em>Optional</em>. Original filename as defined by sender
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FileName { get; set; }

    /// <summary>
    /// <em>Optional</em>. MIME type of the file as defined by sender
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MimeType { get; set; }
}
