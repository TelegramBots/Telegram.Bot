using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a sticker.
    /// <see href="https://core.telegram.org/bots/api#sticker"/>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Sticker : File
    {
        /// <summary>
        /// Sticker width
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Width { get; set; }

        /// <summary>
        /// Sticker height
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Height { get; set; }

        /// <summary>
        /// Sticker thumbnail in .webp or .jpg format
        /// </summary>
        [JsonProperty]
        public PhotoSize Thumb { get; set; }

        /// <summary>
        /// Emoji associated with the sticker
        /// </summary>
        [JsonProperty]
        public string Emoji { get; set; }

        /// <summary>
        /// Optional. Name of the sticker set to which the sticker belongs
        /// </summary>
        [JsonProperty]
        public string SetName { get; set; }

        /// <summary>
        /// Optional. For mask stickers, the position where the mask should be placed
        /// </summary>
        [JsonProperty]
        public MaskPosition MaskPosition { get; set; }
    }
}
