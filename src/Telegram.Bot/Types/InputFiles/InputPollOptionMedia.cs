// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents the content of a poll option to be sent. It should be one of<br/><see cref="InputMediaAnimation"/>, <see cref="InputMediaLink"/>, <see cref="InputMediaLivePhoto"/>, <see cref="InputMediaLocation"/>, <see cref="InputMediaPhoto"/>, <see cref="InputMediaSticker"/>, <see cref="InputMediaVenue"/>, <see cref="InputMediaVideo"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<InputPollOptionMedia>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InputMediaAnimation), "animation")]
[CustomJsonDerivedType(typeof(InputMediaLink), "link")]
[CustomJsonDerivedType(typeof(InputMediaLivePhoto), "live_photo")]
[CustomJsonDerivedType(typeof(InputMediaLocation), "location")]
[CustomJsonDerivedType(typeof(InputMediaPhoto), "photo")]
[CustomJsonDerivedType(typeof(InputMediaSticker), "sticker")]
[CustomJsonDerivedType(typeof(InputMediaVenue), "venue")]
[CustomJsonDerivedType(typeof(InputMediaVideo), "video")]
#pragma warning disable CA1715, IDE1006
public interface InputPollOptionMedia
{
    /// <summary>Type of the media</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract InputMediaType Type { get; }
}
