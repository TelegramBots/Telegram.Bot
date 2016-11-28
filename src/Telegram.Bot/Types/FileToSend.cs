using System;
using System.IO;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents information for a file to be sent
    /// </summary>
    public struct FileToSend
    {
        /// <summary>
        /// Filename for uploaded file.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// File content for uploaded file.
        /// </summary>
        public Stream Content { get; set; }

        /// <summary>
        /// File Uri.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Send File by Id
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// Type of file to send
        /// </summary>
        public FileType Type {
            get
            {
                if (Content != null) return FileType.Stream;
                else if (FileId != null) return FileType.Id;
                else if (Url != null) return FileType.Url;
                else return FileType.Unknown;
            }
        }

        /// <summary>
        /// Send a FileStream.
        /// </summary>
        /// <param name="filename">The <see cref="Filename"/>.</param>
        /// <param name="content">The <see cref="Content"/>.</param>
        public FileToSend(string filename, Stream content)
        {
            Filename = filename;
            Content = content;

            Url = null;
            FileId = null;
        }

        /// <summary>
        /// Send a File from Url
        /// </summary>
        /// <param name="url">The File to send</param>
        public FileToSend(Uri url)
        {
            Url = url;

            Filename = null;
            Content = null;
            FileId = null;
        }

        /// <summary>
        /// Send a File by Id
        /// </summary>
        /// <param name="fileId">The File to send</param>
        public FileToSend(string fileId)
        {
            FileId = fileId;

            Filename = null;
            Content = null;
            Url = null;
        }
    }
}
