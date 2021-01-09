using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.AddStickerToSetAsync" /> method.
    /// </summary>
    public class AddStickerToSetParameters : ParametersBase
    {
        /// <summary>
        ///     User identifier of sticker set owner
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Sticker set name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed 512px, and either width
        ///     or height must be exactly 512px.
        /// </summary>
        public InputOnlineFile PngSticker { get; set; }

        /// <summary>
        ///     One or more emoji corresponding to the sticker
        /// </summary>
        public string Emojis { get; set; }

        /// <summary>
        ///     Position where the mask should be placed on faces
        /// </summary>
        public MaskPosition MaskPosition { get; set; }

        /// <summary>
        ///     Sets <see cref="UserId" /> property.
        /// </summary>
        /// <param name="userId">User identifier of sticker set owner</param>
        public AddStickerToSetParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Name" /> property.
        /// </summary>
        /// <param name="name">Sticker set name</param>
        public AddStickerToSetParameters WithName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="PngSticker" /> property.
        /// </summary>
        /// <param name="pngSticker">
        ///     Png image with the sticker, must be up to 512 kilobytes in size, dimensions must not exceed
        ///     512px, and either width or height must be exactly 512px.
        /// </param>
        public AddStickerToSetParameters WithPngSticker(InputOnlineFile pngSticker)
        {
            PngSticker = pngSticker;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Emojis" /> property.
        /// </summary>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        public AddStickerToSetParameters WithEmojis(string emojis)
        {
            Emojis = emojis;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="MaskPosition" /> property.
        /// </summary>
        /// <param name="maskPosition">Position where the mask should be placed on faces</param>
        public AddStickerToSetParameters WithMaskPosition(MaskPosition maskPosition)
        {
            MaskPosition = maskPosition;
            return this;
        }
    }
}