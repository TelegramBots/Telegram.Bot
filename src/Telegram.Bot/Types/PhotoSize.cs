using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Converters;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents one size of a photo or a file / sticker thumbnail.
    /// </summary>
    /// <remarks>A missing thumbnail for a file (or sticker) is presented as an empty object.</remarks>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    [JsonConverter(typeof(PhotoSizeConverter))]
    public class PhotoSize : FileBase
    {
        /// <summary>
        /// Photo width
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Width { get; set; }

        /// <summary>
        /// Photo height
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Height { get; set; }
    }
}
