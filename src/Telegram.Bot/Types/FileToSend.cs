using System.IO;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents information for a file to be sent
    /// </summary>
    public struct FileToSend
    {
        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public Stream Content { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileToSend"/> struct.
        /// </summary>
        /// <param name="filename">The <see cref="Filename"/>.</param>
        /// <param name="content">The <see cref="Content"/>.</param>
        public FileToSend(string filename, Stream content)
        {
            Filename = filename;
            Content = content;
        }
    }
}
