// ReSharper disable once CheckNamespace

namespace Telegram.Bot.Types;

/// <summary>
/// Indicates that an <see cref="InputMediaBase"/> has a thumbnail.
/// </summary>
public interface IInputMediaThumb
{
    /// <summary>
    /// Optional. Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported
    /// server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and
    /// height should not exceed 320. Ignored if the file is not uploaded using multipart/form-data.
    /// </summary>
    InputMedia? Thumb { get; }
}