// ReSharper disable once CheckNamespace

using System;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the chat does not exist
    /// </summary>
    [Obsolete("Description message for this error has changed and this exception type will never be thrown.")]
    public class ContactRequestException : BadRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="ChatNotFoundException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        public ContactRequestException(string message)
            : base(message)
        {
        }
    }
}
