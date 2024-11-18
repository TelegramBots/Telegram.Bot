
// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>A marker for input media types that can be used in sendMediaGroup method.</summary>
[JsonConverter(typeof(PolymorphicJsonConverter<IAlbumInputMedia>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(InputMediaDocument), "document")]
[CustomJsonDerivedType(typeof(InputMediaAudio), "audio")]
[CustomJsonDerivedType(typeof(InputMediaPhoto), "photo")]
[CustomJsonDerivedType(typeof(InputMediaVideo), "video")]
public interface IAlbumInputMedia;
