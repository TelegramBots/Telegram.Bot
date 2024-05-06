using Telegram.Bot.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>
/// A marker for input media types that can be used in sendMediaGroup method.
/// </summary>
// [todo] maybe implement converter
/*
[CustomJsonPolymorphic]
[CustomJsonDerivedType(typeof(InputMediaAudio))]
[CustomJsonDerivedType(typeof(InputMediaDocument))]
[CustomJsonDerivedType(typeof(InputMediaPhoto))]
[CustomJsonDerivedType(typeof(InputMediaVideo))]
*/
public interface IAlbumInputMedia;
