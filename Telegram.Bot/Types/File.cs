using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class File
    {
        /// <summary>
        /// Unique identifier for this file
        /// </summary>
        [JsonProperty(PropertyName = "file_id", Required = Required.Always)]
        public string FileId;

        /// <summary>
        /// Optional. File size
        /// </summary>
        [JsonProperty(PropertyName = "file_size", Required = Required.Default)]
        public int FileSize;
    }
}
