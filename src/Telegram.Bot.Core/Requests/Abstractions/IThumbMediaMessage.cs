using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Indicates a message has thumbnail
    /// </summary>
    public interface IThumbMediaMessage
    {
        /// <summary>
        /// Thumbnail of the file sent. The thumbnail should be in JPEG format and less than 200 kB in size. A
        /// thumbnail's width and height should not exceed 90. Thumbnails can't be reused and can be only uploaded as
        /// a new file.
        /// </summary>
        InputMedia Thumb { get; set; }
    }
}
