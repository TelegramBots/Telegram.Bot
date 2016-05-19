using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a sticker.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Sticker : File
    {
        /// <summary>
        /// Sticker width
        /// </summary>
        [JsonProperty(PropertyName = "width", Required = Required.Always)]
        public string Width { get; internal set; }

        /// <summary>
        /// Sticker height
        /// </summary>
        [JsonProperty(PropertyName = "height", Required = Required.Always)]
        public string Height { get; internal set; }

        /// <summary>
        /// Sticker thumbnail in .webp or .jpg format
        /// </summary>
        [JsonProperty(PropertyName = "thumb", Required = Required.Default)]
        public PhotoSize Thumb { get; internal set; }

        /// <summary>
        /// Emoji associated with the sticker
        /// </summary>
        [JsonProperty(PropertyName = "emoji", Required = Required.Default)]
        public string Emoji { get; internal set; }
    }
}
