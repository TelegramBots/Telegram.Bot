using Newtonsoft.Json;
using System.IO;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a file ready to be downloaded. The file can be downloaded via <see cref="TelegramBotClient.GetFileAsync"/>. It is guaranteed that the link will be valid for at least 1 hour. When the link expires, a new one can be requested by calling <see cref="TelegramBotClient.GetFileAsync"/>.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class File
    {
        /// <summary>
        /// Unique identifier for this file
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string FileId { get; set; }

        /// <summary>
        /// Optional. File size, if known
        /// </summary>
        [JsonProperty]
        public int FileSize { get; set; }

        /// <summary>
        /// File path. Use <see cref="TelegramBotClient.GetFileAsync"/> to get the file.
        /// </summary>
        [JsonProperty]
        public string FilePath { get; set; }

        /// <summary>
        /// Gets the file stream.
        /// </summary>
        public Stream FileStream { get; set; }
    }
}
