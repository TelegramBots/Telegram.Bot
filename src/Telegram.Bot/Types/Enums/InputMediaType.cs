namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Type of the input media
/// </summary>
[JsonConverter(typeof(InputMediaTypeConverter))]
public enum InputMediaType
{
    /// <summary>
    /// Photo
    /// </summary>
    Photo = 1,

    /// <summary>
    /// Video
    /// </summary>
    Video,

    /// <summary>
    /// Animation
    /// </summary>
    Animation,

    /// <summary>
    /// Audio
    /// </summary>
    Audio,

    /// <summary>
    /// Document
    /// </summary>
    Document
}
