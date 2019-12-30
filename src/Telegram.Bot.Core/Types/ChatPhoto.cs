namespace Telegram.Bot.Types
{
    /// <summary>
    /// Collection of fileIds of profile pictures of a chat.
    /// </summary>
    public class ChatPhoto
    {
        /// <summary>
        /// File id of the big version of this <see cref="ChatPhoto"/>
        /// </summary>
        public string BigFileId { get; set; }

        /// <summary>
        /// File id of the small version of this <see cref="ChatPhoto"/>
        /// </summary>
        public string SmallFileId { get; set; }
    }
}
