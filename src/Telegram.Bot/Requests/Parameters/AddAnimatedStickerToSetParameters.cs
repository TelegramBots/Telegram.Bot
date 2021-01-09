using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.AddAnimatedStickerToSetAsync" /> method.
    /// </summary>
    public class AddAnimatedStickerToSetParameters : ParametersBase
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
        ///     Tgs animation with the sticker
        /// </summary>
        public InputFileStream TgsSticker { get; set; }

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
        public AddAnimatedStickerToSetParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Name" /> property.
        /// </summary>
        /// <param name="name">Sticker set name</param>
        public AddAnimatedStickerToSetParameters WithName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="TgsSticker" /> property.
        /// </summary>
        /// <param name="tgsSticker">Tgs animation with the sticker</param>
        public AddAnimatedStickerToSetParameters WithTgsSticker(InputFileStream tgsSticker)
        {
            TgsSticker = tgsSticker;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Emojis" /> property.
        /// </summary>
        /// <param name="emojis">One or more emoji corresponding to the sticker</param>
        public AddAnimatedStickerToSetParameters WithEmojis(string emojis)
        {
            Emojis = emojis;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="MaskPosition" /> property.
        /// </summary>
        /// <param name="maskPosition">Position where the mask should be placed on faces</param>
        public AddAnimatedStickerToSetParameters WithMaskPosition(MaskPosition maskPosition)
        {
            MaskPosition = maskPosition;
            return this;
        }
    }
}