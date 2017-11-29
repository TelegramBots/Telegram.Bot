using System.IO;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents the contents of a file to be uploaded. Must be posted using multipart/form-data in the usual way that files are uploaded via the browser
    /// </summary>
    public abstract class InputFileBase
    {
        /// <summary>
        /// Id of a file that exists on the Telegram servers
        /// </summary>
        public string FileId { get; protected set; }

        /// <summary>
        /// HTTP URL for Telegram to get a file from the Internet
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Name of a file to upload using multipart/form-data
        /// </summary>
        public string FileName { get; protected set; }

        /// <summary> 
        /// File content to upload
        /// </summary>
        public Stream Content { get; protected set; }

        /// <summary>
        /// Type of file to send
        /// </summary>
        public FileType FileType
        {
            get
            {
                if (Content != null) return FileType.Stream;
                if (FileId != null) return FileType.Id;
                if (Url != null) return FileType.Url;
                return FileType.Unknown;
            }
        }
    }
}
