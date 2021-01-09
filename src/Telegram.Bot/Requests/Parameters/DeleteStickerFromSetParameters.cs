namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.DeleteStickerFromSetAsync" /> method.
    /// </summary>
    public class DeleteStickerFromSetParameters : ParametersBase
    {
        /// <summary>
        ///     File identifier of the sticker
        /// </summary>
        public string Sticker { get; set; }

        /// <summary>
        ///     Sets <see cref="Sticker" /> property.
        /// </summary>
        /// <param name="sticker">File identifier of the sticker</param>
        public DeleteStickerFromSetParameters WithSticker(string sticker)
        {
            Sticker = sticker;
            return this;
        }
    }
}