using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// Reperesents an api exception when the bot is unable to find an user
    /// </summary>
    public class UserNotFoundException : ApiRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="UserNotFoundException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        public UserNotFoundException(string message) : base(message, 400) { }
    }
}