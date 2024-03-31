using Telegram.Bot.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A marker for input media types that can be used in sendMediaGroup method.
/// </summary>
[CustomJsonPolymorphic]
[CustomJsonDerivedType<InputMediaAudio>]
[CustomJsonDerivedType<InputMediaDocument>]
[CustomJsonDerivedType<InputMediaPhoto>]
[CustomJsonDerivedType<InputMediaVideo>]
public interface IAlbumInputMedia;
