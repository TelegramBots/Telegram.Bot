namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.DownloadFileAsync" /> method.
    /// </summary>
    public class DownloadFileParameters : ParametersBase
    {
        /// <summary>
        ///     Path to file on server
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        ///     Sets <see cref="FilePath" /> property.
        /// </summary>
        /// <param name="filePath">Path to file on server</param>
        public DownloadFileParameters WithFilePath(string filePath)
        {
            FilePath = filePath;
            return this;
        }
    }
}