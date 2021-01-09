using Telegram.Bot.Types.InputFiles;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.UploadStickerFileAsync" /> method.
    /// </summary>
    public class UploadStickerFileParameters : ParametersBase
    {
        /// <summary>
        ///     User identifier of sticker file owner
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width
        ///     or height must be exactly 512px.
        /// </summary>
        public InputFileStream PngSticker { get; set; }

        /// <summary>
        ///     Sets <see cref="UserId" /> property.
        /// </summary>
        /// <param name="userId">User identifier of sticker file owner</param>
        public UploadStickerFileParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="PngSticker" /> property.
        /// </summary>
        /// <param name="pngSticker">
        ///     Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed
        ///     512px, and either width or height must be exactly 512px.
        /// </param>
        public UploadStickerFileParameters WithPngSticker(InputFileStream pngSticker)
        {
            PngSticker = pngSticker;
            return this;
        }
    }
}