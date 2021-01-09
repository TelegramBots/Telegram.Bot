using System.IO;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.DownloadFileAsync" /> method.
    /// </summary>
    public class DownloadFileExtendedParameters : ParametersBase
    {
        /// <summary>
        ///     Path to file on server
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        ///     Destination stream to write file to
        /// </summary>
        public Stream Destination { get; set; }

        /// <summary>
        ///     Sets <see cref="FilePath" /> property.
        /// </summary>
        /// <param name="filePath">Path to file on server</param>
        public DownloadFileExtendedParameters WithFilePath(string filePath)
        {
            FilePath = filePath;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Destination" /> property.
        /// </summary>
        /// <param name="destination">Destination stream to write file to</param>
        public DownloadFileExtendedParameters WithDestination(Stream destination)
        {
            Destination = destination;
            return this;
        }
    }
}
