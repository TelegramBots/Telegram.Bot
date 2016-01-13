using System.IO;
using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a file ready to be downloaded. The file can be downloaded via the link https://api.telegram.org/file/bot{token}/{file_path}. It is guaranteed that the link will be valid for at least 1 hour. When the link expires, a new one can be requested by calling getFile.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class File
    {
        /// <summary>
        /// Unique identifier for this file
        /// </summary>
        [JsonProperty(PropertyName = "file_id", Required = Required.Always)]
        public string FileId { get; internal set; }

        /// <summary>
        /// Optional. File size, if known
        /// </summary>
        [JsonProperty(PropertyName = "file_size", Required = Required.Default)]
        public int FileSize { get; internal set; }

        /// <summary>
        /// File path. Use https://api.telegram.org/file/bot{token}/{file_path} to get the file.
        /// </summary>
        [JsonProperty(PropertyName = "file_path", Required = Required.Default)]
        public string FilePath { get; internal set; }

        public Stream FileStream { get; internal set; }
    }
}
