// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>The type of <see cref="PollMedia"/></summary>
[JsonConverter(typeof(EnumConverter<PollMediaType>))]
public enum PollMediaType
{
    /// <summary><see cref="PollMedia"/> type is unknown</summary>
    Unknown = 0,
    /// <summary>The <see cref="PollMedia"/> contains an <see cref="PollMedia.Animation"/></summary>
    Animation,
    /// <summary>The <see cref="PollMedia"/> contains an <see cref="PollMedia.Audio"/></summary>
    Audio,
    /// <summary>The <see cref="PollMedia"/> contains a <see cref="PollMedia.Document"/></summary>
    Document,
    /// <summary>The <see cref="PollMedia"/> contains a <see cref="PollMedia.LivePhoto"/></summary>
    LivePhoto,
    /// <summary>The <see cref="PollMedia"/> contains a <see cref="PollMedia.Location"/></summary>
    Location,
    /// <summary>The <see cref="PollMedia"/> contains a <see cref="PollMedia.Photo"/></summary>
    Photo,
    /// <summary>The <see cref="PollMedia"/> contains a <see cref="PollMedia.Sticker"/></summary>
    Sticker,
    /// <summary>The <see cref="PollMedia"/> contains a <see cref="PollMedia.Venue"/></summary>
    Venue,
    /// <summary>The <see cref="PollMedia"/> contains a <see cref="PollMedia.Video"/></summary>
    Video,
    /// <summary>The <see cref="PollMedia"/> contains a <see cref="PollMedia.Link"/></summary>
    Link,
}
