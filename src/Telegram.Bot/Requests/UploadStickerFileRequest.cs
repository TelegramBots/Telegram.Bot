using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;
using File = Telegram.Bot.Types.File;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Upload a .png file with a sticker for later use in createNewStickerSet and addStickerToSet methods (can be used multiple times). Returns the uploaded <see cref="File"/> on success.
    /// </summary>
    public class UploadStickerFileRequest : RequestBase<File>
    {
        /// <summary>
        /// User identifier of sticker file owner
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.
        /// </summary>
        public Stream PngSticker { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public UploadStickerFileRequest()
            : base("uploadStickerFile")
        { }

        /// <summary>
        /// Initializes a new request with userId and pngSticker
        /// </summary>
        public UploadStickerFileRequest(int userId, Stream pngSticker)
            : this()
        {
            UserId = userId;
            PngSticker = pngSticker;
        }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        public override HttpContent ToHttpContent(JsonSerializerSettings serializerSettings)
        {
            var multipartContent = new MultipartFormDataContent(Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks)
            {
                { new StringContent(UserId.ToString()), nameof(UserId).ToSnakeCased() }
            };

            if (PngSticker != null)
            {
                multipartContent.AddStreamContent(PngSticker, nameof(PngSticker).ToSnakeCased());
            }

            return multipartContent;
        }
    }
}
