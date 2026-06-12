// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the media</summary>
[JsonConverter(typeof(EnumConverter<InputPollOptionMediaType>))]
public enum InputPollOptionMediaType
{
    /// <summary>Represents an animation file (GIF or H.264/MPEG-4 AVC video without sound) to be sent.<br/><br/><i>(<see cref="InputPollOptionMedia"/> can be cast into <see cref="InputMediaAnimation"/>)</i></summary>
    Animation = 1,
    /// <summary>Represents a live photo to be sent.<br/><br/><i>(<see cref="InputPollOptionMedia"/> can be cast into <see cref="InputMediaLivePhoto"/>)</i></summary>
    LivePhoto,
    /// <summary>Represents a location to be sent.<br/><br/><i>(<see cref="InputPollOptionMedia"/> can be cast into <see cref="InputMediaLocation"/>)</i></summary>
    Location,
    /// <summary>Represents a photo to be sent.<br/><br/><i>(<see cref="InputPollOptionMedia"/> can be cast into <see cref="InputMediaPhoto"/>)</i></summary>
    Photo,
    /// <summary>Represents a sticker file to be sent.<br/><br/><i>(<see cref="InputPollOptionMedia"/> can be cast into <see cref="InputMediaSticker"/>)</i></summary>
    Sticker,
    /// <summary>Represents a venue to be sent.<br/><br/><i>(<see cref="InputPollOptionMedia"/> can be cast into <see cref="InputMediaVenue"/>)</i></summary>
    Venue,
    /// <summary>Represents a video to be sent.<br/><br/><i>(<see cref="InputPollOptionMedia"/> can be cast into <see cref="InputMediaVideo"/>)</i></summary>
    Video,
    /// <summary>Represents an HTTP link to be sent.<br/><br/><i>(<see cref="InputPollOptionMedia"/> can be cast into <see cref="InputMediaLink"/>)</i></summary>
    Link,
}
