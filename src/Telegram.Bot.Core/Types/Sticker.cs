namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a sticker.
    /// <see href="https://core.telegram.org/bots/api#sticker"/>
    /// </summary>
    public class Sticker : FileBase
    {
        /// <summary>
        /// Sticker width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Sticker height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// True, if the sticker is animated
        /// </summary>
        public bool IsAnimated { get; set; }

        /// <summary>
        /// Sticker thumbnail in .webp or .jpg format
        /// </summary>
        public PhotoSize Thumb { get; set; }

        /// <summary>
        /// Emoji associated with the sticker
        /// </summary>
        public string Emoji { get; set; }

        /// <summary>
        /// Optional. Name of the sticker set to which the sticker belongs
        /// </summary>
        public string SetName { get; set; }

        /// <summary>
        /// Optional. For mask stickers, the position where the mask should be placed
        /// </summary>
        public MaskPosition MaskPosition { get; set; }
    }
}
