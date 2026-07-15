// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the media</summary>
[JsonConverter(typeof(EnumConverter<InputMediaType>))]
public enum InputMediaType
{
    /// <summary>Represents a photo to be sent.<br/><br/><i>(<see cref="InputMedia"/> can be cast into <see cref="InputMediaPhoto"/>)</i></summary>
    Photo = 1,
    /// <summary>Represents a video to be sent.<br/><br/><i>(<see cref="InputMedia"/> can be cast into <see cref="InputMediaVideo"/>)</i></summary>
    Video,
    /// <summary>Represents an animation file (GIF or H.264/MPEG-4 AVC video without sound) to be sent.<br/><br/><i>(<see cref="InputMedia"/> can be cast into <see cref="InputMediaAnimation"/>)</i></summary>
    Animation,
    /// <summary>Represents an audio file to be treated as music to be sent.<br/><br/><i>(<see cref="InputMedia"/> can be cast into <see cref="InputMediaAudio"/>)</i></summary>
    Audio,
    /// <summary>Represents a general file to be sent.<br/><br/><i>(<see cref="InputMedia"/> can be cast into <see cref="InputMediaDocument"/>)</i></summary>
    Document,
    /// <summary>Represents a live photo to be sent.<br/><br/><i>(<see cref="InputMedia"/> can be cast into <see cref="InputMediaLivePhoto"/>)</i></summary>
    LivePhoto,
    /// <summary>Represents a location to be sent.<br/><br/><i>(InputPoll*Media can be cast into <see cref="InputMediaLocation"/>)</i></summary>
    Location,
    /// <summary>Represents a sticker file to be sent.<br/><br/><i>(InputPoll*Media can be cast into <see cref="InputMediaSticker"/>)</i></summary>
    Sticker,
    /// <summary>Represents a venue to be sent.<br/><br/><i>(InputPoll*Media can be cast into <see cref="InputMediaVenue"/>)</i></summary>
    Venue,
    /// <summary>Represents an HTTP link to be sent.<br/><br/><i>(InputPoll*Media can be cast into <see cref="InputMediaLink"/>)</i></summary>
    Link,
    /// <summary>Represents a voice message file to be sent.<br/><br/><i>(InputPoll*Media can be cast into <see cref="InputMediaVoiceNote"/>)</i></summary>
    VoiceNote,
}
