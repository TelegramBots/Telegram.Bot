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
        public string Width;

        /// <summary>
        /// Sticker height
        /// </summary>
        [JsonProperty(PropertyName = "height", Required = Required.Always)]
        public string Height;

        /// <summary>
        /// Sticker thumbnail in .webp or .jpg format
        /// </summary>
        [JsonProperty(PropertyName = "thumb", Required = Required.Default)]
        public PhotoSize Thumb;
    }
}
