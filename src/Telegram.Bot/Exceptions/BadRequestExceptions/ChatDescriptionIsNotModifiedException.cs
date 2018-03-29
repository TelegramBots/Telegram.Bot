// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception is thrown when the chat description is not modified
    /// </summary>
    public class ChatDescriptionIsNotModifiedException : BadRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="ChatDescriptionIsNotModifiedException"/> class
        /// </summary>
        /// <param name="message">The message of this exception</param>
        public ChatDescriptionIsNotModifiedException(string message)
            : base(message)
        {
        }
    }
}
