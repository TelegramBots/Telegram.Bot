// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the chat parameters is not modified during request
    /// </summary>
    public class ChatNotModifiedException : BadRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="ChatNotModifiedException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        public ChatNotModifiedException(string message)
            : base(message)
        {
        }
    }
}