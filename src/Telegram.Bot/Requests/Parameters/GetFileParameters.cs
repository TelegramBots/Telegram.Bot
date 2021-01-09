namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.GetFileAsync" /> method.
    /// </summary>
    public class GetFileParameters : ParametersBase
    {
        /// <summary>
        ///     File identifier
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        ///     Sets <see cref="FileId" /> property.
        /// </summary>
        /// <param name="fileId">File identifier</param>
        public GetFileParameters WithFileId(string fileId)
        {
            FileId = fileId;
            return this;
        }
    }
}
