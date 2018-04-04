// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception is thrown when the when bot sends message to the supergroup or channel in which it was not added
    /// </summary>
    public class BotIsNotMemberException : ForbiddenException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="BotIsNotMemberException"/> class
        /// </summary>
        /// <param name="message">The message of this exception</param>
        public BotIsNotMemberException(string message)
            : base(message)
        {
        }
    }
}
