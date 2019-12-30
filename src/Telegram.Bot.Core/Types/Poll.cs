namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object contains information about a poll.
    /// </summary>
    public class Poll
    {
        /// <summary>
        /// Unique poll identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Poll question, 1-255 characters
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// List of poll options
        /// </summary>
        public PollOption[] Options { get; set; }

        /// <summary>
        /// True, if the poll is closed
        /// </summary>
        public bool IsClosed { get; set; }
    }
}
