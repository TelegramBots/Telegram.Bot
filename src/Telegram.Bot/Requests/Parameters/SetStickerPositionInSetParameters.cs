namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetStickerPositionInSetAsync" /> method.
    /// </summary>
    public class SetStickerPositionInSetParameters : ParametersBase
    {
        /// <summary>
        ///     File identifier of the sticker
        /// </summary>
        public string Sticker { get; set; }

        /// <summary>
        ///     New sticker position in the set, zero-based
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        ///     Sets <see cref="Sticker" /> property.
        /// </summary>
        /// <param name="sticker">File identifier of the sticker</param>
        public SetStickerPositionInSetParameters WithSticker(string sticker)
        {
            Sticker = sticker;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Position" /> property.
        /// </summary>
        /// <param name="position">New sticker position in the set, zero-based</param>
        public SetStickerPositionInSetParameters WithPosition(int position)
        {
            Position = position;
            return this;
        }
    }
}
