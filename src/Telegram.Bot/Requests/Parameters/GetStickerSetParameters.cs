namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.GetStickerSetAsync" /> method.
    /// </summary>
    public class GetStickerSetParameters : ParametersBase
    {
        /// <summary>
        ///     Short name of the sticker set that is used in t.me/addstickers/ URLs (e.g., animals)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Sets <see cref="Name" /> property.
        /// </summary>
        /// <param name="name">Short name of the sticker set that is used in t.me/addstickers/ URLs (e.g., animals)</param>
        public GetStickerSetParameters WithName(string name)
        {
            Name = name;
            return this;
        }
    }
}
