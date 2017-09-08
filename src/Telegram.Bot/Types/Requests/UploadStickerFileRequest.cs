using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to upload a sticker file for later use
    /// </summary>
    public class UploadStickerFileRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadStickerFileRequest"/> class
        /// </summary>
        /// <param name="userId">User identifier of sticker file owner</param>
        /// <param name="pngSticker">Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.</param>
        public UploadStickerFileRequest(int userId, FileToSend pngSticker) : base("uploadStickerFile",
            new Dictionary<string, object> { { "user_id", userId } })
        {
            switch (pngSticker.Type)
            {
                case FileType.Stream:
                    FileStream = pngSticker.Content;
                    FileName = pngSticker.Filename;
                    FileParameterName = "png_sticker";
                    break;
                default:
                    Parameters.Add("png_sticker", pngSticker);
                    break;
            }
        }
    }
}
