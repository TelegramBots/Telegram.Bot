// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types;

/// <summary>Indicates that an <see cref="InputMedia"/> has a thumbnail.</summary>
public interface IInputMediaThumb
{
	/// <summary>Optional. Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></summary>
	InputFile? Thumbnail { get; }
}
