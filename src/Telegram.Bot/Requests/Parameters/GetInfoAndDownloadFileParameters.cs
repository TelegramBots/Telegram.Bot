using System.IO;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.GetInfoAndDownloadFileAsync" /> method.
    /// </summary>
    public class GetInfoAndDownloadFileParameters : ParametersBase
    {
        /// <summary>
        ///     File identifier to get info about
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        ///     Destination stream to write file to
        /// </summary>
        public Stream Destination { get; set; }

        /// <summary>
        ///     Sets <see cref="FileId" /> property.
        /// </summary>
        /// <param name="fileId">File identifier to get info about</param>
        public GetInfoAndDownloadFileParameters WithFileId(string fileId)
        {
            FileId = fileId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Destination" /> property.
        /// </summary>
        /// <param name="destination">Destination stream to write file to</param>
        public GetInfoAndDownloadFileParameters WithDestination(Stream destination)
        {
            Destination = destination;
            return this;
        }
    }
}
