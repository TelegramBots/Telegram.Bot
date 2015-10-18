using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one size of a photo or a file / sticker thumbnail.
    /// </summary>
    /// <remarks>A missing thumbnail for a file (or sticker) is presented as an empty object.</remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class PhotoSize : File
    {
        /// <summary>
        /// Photo width
        /// </summary>
        [JsonProperty(PropertyName = "width", Required = Required.Always)]
        public int Width { get; set; }

        /// <summary>
        /// Photo height
        /// </summary>
        [JsonProperty(PropertyName = "Height", Required = Required.Always)]
        public int Height { get; set; }
    }
}
