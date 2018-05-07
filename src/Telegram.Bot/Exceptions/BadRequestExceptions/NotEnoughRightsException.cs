// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception is thrown when the bot has not enough rights to do something
    /// </summary>
    public class NotEnoughRightsException : BadRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="NotEnoughRightsException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception</param>
        public NotEnoughRightsException(string message) 
            : base(message)
        {
        }
    }
}
