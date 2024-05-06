namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a <a href="https://telegram.org/blog/video-messages-and-telescope">video message</a>
/// (available in Telegram apps as of
/// <a href="https://telegram.org/blog/video-messages-and-telescope">v.4.0</a>).
/// </summary>
public class VideoNote : FileBase
{
    /// <summary>
    /// Video width and height (diameter of the video message) as defined by sender
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Length { get; set; }

    /// <summary>
    /// Duration of the video in seconds as defined by sender
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Duration { get; set; }

    /// <summary>
    /// Optional. Video thumbnail
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PhotoSize? Thumbnail { get; set; }
}
