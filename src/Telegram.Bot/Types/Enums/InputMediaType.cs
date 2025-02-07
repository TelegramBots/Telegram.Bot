// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of the result</summary>
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
}
