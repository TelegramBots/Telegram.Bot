namespace Telegram.Bot.Types;

/// <summary>A marker for input media types that can be used as <see cref="InputRichMessageMedia.Media">Media</see> for the <see cref="InputRichMessage.Media"/> entries.</summary>
[JsonConverter(typeof(PolymorphicJsonConverter<IInputRichMedia>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InputMediaAnimation), "animation")]
[CustomJsonDerivedType(typeof(InputMediaAudio), "audio")]
[CustomJsonDerivedType(typeof(InputMediaPhoto), "photo")]
[CustomJsonDerivedType(typeof(InputMediaVideo), "video")]
[CustomJsonDerivedType(typeof(InputMediaVoiceNote), "voice_note")]
public interface IInputRichMedia
{
    /// <summary>Type of the media</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract InputMediaType Type { get; }
}
