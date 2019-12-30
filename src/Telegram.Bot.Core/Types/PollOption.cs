namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object contains information about one answer option in a poll.
    /// </summary>
    public class PollOption
    {
        /// <summary>
        /// Option text, 1-100 characters
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Number of users that voted for this option
        /// </summary>
        public int VoterCount { get; set; }
    }
}
