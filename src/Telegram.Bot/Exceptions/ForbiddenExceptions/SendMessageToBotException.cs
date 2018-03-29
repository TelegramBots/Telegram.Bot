// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception is thrown when the bot sends message to another bot
    /// </summary>
    public class SendMessageToBotException : ForbiddenException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="SendMessageToBotException"/> class
        /// </summary>
        /// <param name="message">The message of this exception</param>
        public SendMessageToBotException(string message)
            : base(message)
        {
        }
    }
}
