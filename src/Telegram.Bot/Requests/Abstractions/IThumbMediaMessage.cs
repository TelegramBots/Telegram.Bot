using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Indicates a message has thumbnail
    /// </summary>
    public interface IThumbMediaMessage
    {
        /// <summary>
        /// Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using multipart/form-data. Thumbnails can't be reused and can be only uploaded as a new file, so you can pass "attach://&lt;file_attach_name&gt;" if the thumbnail was uploaded using multipart/form-data under &lt;file_attach_name&gt;.
        /// </summary>
        InputMedia? Thumb { get; set; }
    }
}
