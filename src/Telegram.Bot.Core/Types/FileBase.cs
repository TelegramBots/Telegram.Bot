using System.Runtime.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a file ready to be downloaded. The file can be downloaded via <see cref="TelegramBotClient.GetFileAsync"/>. It is guaranteed that the link will be valid for at least 1 hour. When the link expires, a new one can be requested by calling <see cref="TelegramBotClient.GetFileAsync"/>.
    /// </summary>
    [DataContract]
    public abstract class FileBase
    {
        /// <summary>
        /// Unique identifier for this file
        /// </summary>
        [DataMember(IsRequired = true)]
        public string FileId { get; set; }

        /// <summary>
        /// Optional. File size, if known
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public int FileSize { get; set; }
    }
}
