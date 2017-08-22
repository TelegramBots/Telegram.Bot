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
        [JsonProperty(PropertyName = "width", Required = Required.Always)]
        public int Width { get; set; }

        /// <summary>
        /// Sticker height
        /// </summary>
        [JsonProperty(PropertyName = "height", Required = Required.Always)]
        public int Height { get; set; }

        /// <summary>
        /// Sticker thumbnail in .webp or .jpg format
        /// </summary>
        [JsonProperty(PropertyName = "thumb", Required = Required.Default)]
        public PhotoSize Thumb { get; set; }

        /// <summary>
        /// Emoji associated with the sticker
        /// </summary>
        [JsonProperty(PropertyName = "emoji", Required = Required.Default)]
        public string Emoji { get; set; }

        /// <summary>
        /// Optional. Name of the sticker set to which the sticker belongs
        /// </summary>
        [JsonProperty(PropertyName = "set_name", Required = Required.Default)]
        public string SetName { get; set; }

        /// <summary>
        /// Optional. For mask stickers, the position where the mask should be placed
        /// </summary>
        [JsonProperty(PropertyName = "mask_position", Required = Required.Default)]
        public MaskPosition MaskPosition { get; set; }
    }
}
