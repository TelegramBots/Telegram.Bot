using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to create a new sticker set
    /// </summary>
    public class CreateNewStickerSetRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewStickerSetRequest"/> class
        /// </summary>
        /// <param name="userId">User identifier of created sticker set owner</param>
        /// <param name="name">Short name of sticker set, to be used in t.me/addstickers/ URLs (e.g., animals). Can contain only english letters, digits and underscores. Must begin with a letter, can't contain consecutive underscores and must end in “_by_&lt;bot_username&gt;”. &lt;bot_username&gt; is case insensitive. 1-64 characters.</param>
        /// <param name="title">Sticker set title, 1-64 characters</param>
        /// <param name="pngSticker">Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width or height must be exactly 512px.</param>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        /// <param name="isMasks">Pass True, if a set of mask stickers should be created</param>
        /// <param name="maskPosition">Position where the mask should be placed on faces</param>
        public CreateNewStickerSetRequest(int userId, string name, string title, FileToSend pngSticker,
            string emojis, bool isMasks = false, MaskPosition maskPosition = null) : base("createNewStickerSet",
                new Dictionary<string, object>()
                {
                    { "user_id", userId },
                    { "name", name },
                    { "title", title },
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

            if (isMasks)
                Parameters.Add("is_masks", true);

            if (maskPosition != null)
                Parameters.Add("mask_position", maskPosition);
        }
    }
}
