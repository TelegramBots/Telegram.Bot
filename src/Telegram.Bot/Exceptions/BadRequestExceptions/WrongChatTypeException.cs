namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception is thrown when the bot sent a request for the wrong chat type
    /// </summary>
    public class WrongChatTypeException : BadRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="WrongChatTypeException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception</param>
        public WrongChatTypeException(string message)
            : base(message)
        {
        }
    }
}
