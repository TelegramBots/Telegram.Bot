using Newtonsoft.Json;
using System;
using System.IO;
using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents information for a file to be sent
    /// </summary>
    [JsonConverter(typeof(FileToSendConverter))]
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
                if (FileId != null) return FileType.Id;
                if (Url != null) return FileType.Url;
                return FileType.Unknown;
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

    /// <summary>
    /// Extension Methods for easier <see cref="FileToSend"/> handling
    /// </summary>
    public static class FileToSendExtensions
    {
        /// <summary>
        /// Convert a <see cref="Stream"/> to a <see cref="FileToSend"/>
        /// </summary>
        /// <param name="stream">The source <see cref="Stream"/></param>
        /// <param name="fileName">The name of the File</param>
        /// <returns></returns>
        public static FileToSend ToFileToSend(this Stream stream, string fileName)
        {
            return new FileToSend
            {
                Content = stream,
                Filename = fileName
            };
        }
    }
}
