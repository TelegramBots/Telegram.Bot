namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// This represents an exception when the bot was not started by an user
    /// </summary>
    public class BotNotStartedException : ApiRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="BotNotStartedException"/> class
        /// </summary>
        /// <param name="message">The message of this exception</param>
        public BotNotStartedException(string message) : base(message, 403) { }
    }
}