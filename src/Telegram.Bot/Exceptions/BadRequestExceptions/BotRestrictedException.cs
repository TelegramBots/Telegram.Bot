// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the bot is restricted in a group
    /// </summary>
    public class BotRestrictedException : BadRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="BotRestrictedException"/> class
        /// </summary>
        /// <param name="message">The error message</param>
        public BotRestrictedException(string message)
            : base(message)
        {
        }
    }
}
