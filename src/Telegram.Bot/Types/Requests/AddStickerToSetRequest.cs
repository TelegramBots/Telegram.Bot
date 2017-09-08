using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to add a sticker to a set
    /// </summary>
    public class AddStickerToSetRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddStickerToSetRequest"/> class
        /// </summary>
        /// <param name="userId">User identifier of sticker set owner</param>
        /// <param name="name">Sticker set name</param>
        /// <param name="pngSticker">Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.</param>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        /// <param name="maskPosition">Position where the mask should be placed on faces</param>
        public AddStickerToSetRequest(int userId, string name, FileToSend pngSticker,
            string emojis, MaskPosition maskPosition = null) : base("addStickerToSet",
                new Dictionary<string, object>()
                {
                    { "user_id", userId },
                    { "name", name },
                    { "emojis", emojis }
                })
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
