using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// Exception being fired when the bot was unable to find the requested chat.
    /// </summary>
    public class ChatNotFoundException : ApiRequestException
    {
        /// <summary>
        /// Initializes a new object of the <see cref="ChatNotFoundException"/> class
        /// </summary>
        /// <param name="message">The error message of this exception.</param>
        public ChatNotFoundException(string message) : base(message, 400) { }
    }
}
