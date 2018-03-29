// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception is thrown when the bot is blocked by the user
    /// </summary>
    public class BotBlockedException : ForbiddenException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="BotBlockedException"/> class
        /// </summary>
        /// <param name="message">The message of this exception</param>
        public BotBlockedException(string message)
            : base(message)
        {
        }
    }
}
