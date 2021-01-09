using Telegram.Bot.Types.InputFiles;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetStickerSetThumbAsync" /> method.
    /// </summary>
    public class SetStickerSetThumbParameters : ParametersBase
    {
        /// <summary>
        ///     Sticker set name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     User identifier of the sticker set owner
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     A PNG image or a TGS animation with the thumbnail
        /// </summary>
        public InputOnlineFile Thumb { get; set; }

        /// <summary>
        ///     Sets <see cref="Name" /> property.
        /// </summary>
        /// <param name="name">Sticker set name</param>
        public SetStickerSetThumbParameters WithName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="UserId" /> property.
        /// </summary>
        /// <param name="userId">User identifier of the sticker set owner</param>
        public SetStickerSetThumbParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Thumb" /> property.
        /// </summary>
        /// <param name="thumb">A PNG image or a TGS animation with the thumbnail</param>
        public SetStickerSetThumbParameters WithThumb(InputOnlineFile thumb)
        {
            Thumb = thumb;
            return this;
        }
    }
}
